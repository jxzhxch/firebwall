# Developing firebwall Modules #

Firebwall modules are written on Microsoft's .NET 2.0 framework.  The developers of this application use VS2010 for development and building, and test in Windows XP and 7.  Loading modules into firebwall requires an installation of firebwall (.3.3 or later) and a DLL of your module.  More on installation later.

This guide is an entry point into module development, and more advanced features are, for now, left for self study.

## Easiest Way to Start ##

schizo decided  to start putting together Visual Studio Templates for developing fireBwall modules.  They allow you to easily debug, as the template even includes the version of fireBwall to test the module on.  The current one at the time of this edit is [fireBwall 0.3.11.0 Module.zip](http://firebwall.googlecode.com/files/fireBwall%200.3.11.0%20Module.zip).

## Project Environment ##
Begin by creating a new Class Library project.  Our module title for this example is Chili Soup, but something more pertinent to the modules intent is preferred.  The first thing you'll want to do is switch the target framework over to .NET 2.0.  This can be done in the project's Properties window.

We'll need a copy of the FirewallModule.dll next to inherit the basic objects necessary for usage.  We will also need to use FirewallModule.dll as a reference.  By this point you should have a copy of firebwall installed; right click on your project and select Add Reference.  Navigate to your firebwall install and select the FirewallModule.dll.

Now that you've got the reference in place, you need to set up your directive:

```
using FM; 
```

And inherit it:

```
public class ChiliSoup : FirewallModule 
```

## Basic Framework ##

You should take this time to look around the FirewallModule.dll's references.  This is your development framework guide for interacting with the firebwall framework.

If you dig around the object's references, you'll note there are a couple abstract classes we need to override.  We'll step through these briefly.

The first thing we'll need to do is the constructor.  Our constructor should accept an INetworkAdapter object, which is the network adapter we bind to.  This is also where we can declare the module name:

```
 public ChiliSoup(INetworkAdapter adapter)
            : base(adapter)
        {
        }
```

You'll also need a base constructor without any arguments:

```
public ChiliSoup()
          : base ()
       {
       }
```

You should also set your module's metadata in the constructor.  This is where you define specifics such as the author (yourself!), the module's name, etcetera.  Creating a private method and calling it from both constructors would be advised.  This information is **required**, though it can be blank.  Your module's tab name is pulled from this information.

```
   //Post 0.3.5.0
   private void Help()
        {
            MetaData.Name = "Chili Soup";
            MetaData.Version = "1.0.0";
            MetaData.HelpString = "";
            MetaData.Description = "Prepares Chili Soup";
            MetaData.Contact = "you@developer.com";
            MetaData.Author = "Name(alias)";
        }
```

```
   //Before (and including) 0.3.5.0
   private void Help()
       {
           moduleName = "Chili Soup";
       }
```

The next method is ModuleStart().  This is called on when the network adapter is loading up modules; anything within is loaded up each time the module is modded into the adapter.  The return type is of type ModuleError, and can be used to return error messages to the main loading class.  As of .3.4 there are two types of module errors; Success and UnknownError.  Because we do not require any special loads upon start for our module, we create a ModuleError object, give it type Success, and return it.

```
  public override ModuleError ModuleStart()
        {
            ModuleError me = new ModuleError();
            me.errorType = ModuleErrorType.Success;
            return me;
        }
```

ModuleStop is the obvious opposite of ModuleStart -- when modules are being detracted from the main routine, they are deconstructed with this method call.  Any tear down you need to do or saving should be done here.

```
  public override ModuleError ModuleStop()
        {
            ModuleError me = new ModuleError();
            me.errorType = ModuleErrorType.Success;
            return me;
        }
```

You'll want an interface for your module for user interaction and setting adjustments.  To do this, we provide the GetControl method.  This can be used to return a UserControl to the application and automatically generates you a new tab under each device.

```
 public override System.Windows.Forms.UserControl GetControl()
        {
            return new ChiliSoupDisplay(this) { Dock = System.Windows.Forms.DockStyle.Fill};
        }
```

In the above example, we're passing ChiliSoupDisplay, an external UserControl we've created, an instance of this ChiliSoup object.  This allows it to interact with the packets and happenings of this module.

The final, and most important, method is the interiorMain.  This method is where packets are sent, processed on, and returned from.  The return type is PacketMainReturn which can be used to specify whether a packet is to be logged, dropped, accepted, etc.

```
public override PacketMainReturn interiorMain(ref Packet in_packet)
```

in\_packet is the packet that is coming in.  It is of type Packet; the base object type.  Packets in firebwall are upwards processing, meaning, low layers are eth/ip and higher layers are ICMP/ARP/etc.  We provide many methods for extracting source and destination MACs, IPs, types/codes, etcetera from packets for flexibility and ease of design in module development.

```
PacketMainReturn pmr = new PacketMainReturn("Chili Soup Module");
pmr.returnType = PacketMainReturnType.Allow;
return pmr;
```

PacketMainReturn is the returning object.  Its constructor takes the module name that it will be logged by.  The return type of a PMR MUST be set, otherwise an exception will be thrown.  By setting it to Allow, we have told the processing adapter to allow the packet to be accepted **in this module**.  Higher loading modules may have conflicting rules.

If we want to log each packet as it comes through our module, we can simply change the returnType line to read:

```
pmr.returnType = PacketMainReturnType.Allow | PacketMainReturnType.Log;
```

Now we need to specify what message to log.  We can do this by setting PMR's log message.

```
pmr.logMessage = "Logging a packet!\n";
```

Now when a packet comes through our module, it will be both allowed and logged with the appropriate message.

### Determining a Packet Type ###

Chances are you want to work with specific packets.  We provide a very simple interface for determining the specifics of a packet.

```
if ( in_packet.GetHighestLayer() == Protocol.ICMP )
{
   ICMPPacket packet = (ICMPPacket)in_packet;
}
```

The above code determines whether the highest layer of the packet is ICMP or not, then creates a new ICMP packet by casting in\_packet to type ICMPPacket.  With this new packet we can determine specifics about ICMP, such as type and code.  We can also retrieve any other information contained within the lower layers, i.e. MAC, source/destination IP, etc.

### Saving and Loading Module Configuration ###

The PersistentData object in the FirewallModule abstract class is used to save and load information.  In ModuleStart, you first need to add this.
```
LoadConfig();
```
This will populate PersistentData with the object deserialized from the configuration file.

Now for creating the class to use for configuration, it must hold all and only the information you want to save between each run of the firewall(like when the computer reboots).  All this data must be serializable.  The class you create will need to have the Serializable tag.
```
[Serializable]
```
That goes right before the class definition.  To save data, in ModuleStop, or whenever you want to save, you should set PersistentData to the object that you are storing all the data to be saved then run SaveConfig.
```
PersistentData = rules;
SaveConfig();
```
That is example save code from the ModuleStop code in BasicFirewallModule.cs

Now, Dictionaries are often used in C#, but .NET 2.0 does not serialize them, so, included in FirewallModule.dll is the SerializableDictionary class.  It should be used instead of a Dictionary inside of your PersistentData class.

## Loading Our Module ##
Once we build our solution (F6 in VS2010), we can navigate to our module's folder, into /bin/release, and find a copy of your module's dynamically linking library.  This is what we need to get firebwall to load our module.  Firebwall loads external modules when it's first booted up; that means to load one of your modules, you need to drop the DLL into the appropriate path and restart firebwall.

On Windows 7, the appropriate path is:

```
%systemdrive%\Users\%username%\My Documents\firebwall\modules\
```

On Windows XP, the appropriate path is:

```
%systemdrive%\Documents and Settings\%username%\My Documents\firebwall\modules\
```

You only need to drop your module's DLL file as well as the FirewallModule.dll into this path.  In this directory you'll also find pcap files, logs, and module configuration files.

## Troubleshooting ##
If your module is not loaded up after you've restarted firebwall, ensure that you've created and built the project on .NET Framework 2.0.  Anything else will not work.

Ensure that you've created both constructors; the application will not bind without both.

Ensure you've overridden all the necessary virtual methods inherited from FirewallModule.

Our code is all open sourced; search through other modules written by firebwall developers for a better understanding.

Leave a comment if you're having any issues, or email the developers.