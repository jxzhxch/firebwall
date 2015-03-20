# Variables #

## adapter ##
  * public
  * INetworkAdapter adapter
  * NetworkAdapter that this instance of the module is running on

## uiControl ##
  * public
  * UserControl
  * Currently unused, depreciated

## moduleName ##
  * public
  * string
  * The visible name of this module, needs to be set

## PersistentData ##
  * public
  * object
  * The object used to hold data between loading and saving of configuration

## Enabled ##
  * public
  * bool
  * If false, the module instance does not process packets, controlled by an interface in the firewall, no need to implement anything using this when creating your own module

# Constructors #
## Empty ##
  * This must be implemented in the new modules.  This is the constructor used for external modules.  Usually only used for setting the moduleName.

## INetworkAdapter ##
  * This is depreciated and used for internal modules.  The external modules are assigned the NetworkAdapter in another method.

# Functions #
## SaveConfig ##
  * public void SaveConfig()
  * Serializes PersistentData and writes it to file

## LoadConfig ##
  * public void LoadConfig()
  * Deserializes the saved data and puts it in PersistentData

## GetControl ##
  * public virtual System.Windows.Forms.UserControl GetControl()
  * Used to create a new instance of the UserControl used to configure and edit your module

## ModuleStart ##
  * public abstract ModuleError ModuleStart()
  * Run when the module starts, must be implemented

## ModuleStop ##
  * public abstract ModuleError ModuleStop()
  * Run when the module stops, must be implemented

## interiorMain ##
  * public abstract PacketMainReturn interiorMain(ref Packet in\_packet)
  * Run for each packet being processed by the module, this is the work horse of the module, and you must implement it