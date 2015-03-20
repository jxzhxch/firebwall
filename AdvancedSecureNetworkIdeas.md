# Introduction #

The idea for this module or modules is to add a layer of security to the local network.  This will make the communication from the trusted nodes to be trusted over information from possible threat nodes.


# Details #

There are many aspects to this module and it will be a project that will be updated for a long time.  Traffic between these nodes will be communicated with the mac address, avoiding issues like ARP Poisoning between trusted nodes.  This communication will be encrypted.  This will also be used to cache data, allowing for large data to be shared without it needing to be accessed online.
Parts of this idea could be used to replace how Windows manages local networks.  With mac based communication, messages are far harder to intercept and even more so harder fake.  This will allow for one firewalled machine to be designated as the router, where the other's traffic are routed through it.  It will also act as the switch, direct communication between firewalled nodes will not be direct.
There will be a key for the private network, which will be used to negotiate the keys for each of the clients.  There will also be a node to node key.  This is so that the router does not have the key that the nodes communicate to each other with, and can not sniff this information.  So each packet between nodes will be encrypted twice, but each packet that leaves the private network through the router will be encrypted once.  This encryption will be seamless to the user.  Packets being sent will be encrypted accordingly, as well as incoming packets be decrypted.

The first part of this module will be to make the communication method secure and seamless.  Afterwards, the sharing/caching and other features will be built into that.  With the MAC based communication, most communication will be based on new protocols that are designed for speed, since the communication method will already have its own security.