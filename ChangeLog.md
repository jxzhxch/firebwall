# 0.3.11.0 #
  * Switched installers (WIX -> NSIS)
  * Installer only installs the module you want
  * All modules are now external
  * Log display update; selectable, and automatically copies the log to    your clipboard on select.  Displays a maximum 1000 logs
  * Addition of fireBwall custom themes and colors
  * Extended the adapter display
  * Added Start Minimized option
  * Added max log file storage
  * Added max pcap log storage
  * Updated popup menu; allows the messages to be selected and display the appropriate window
  * Changed the updater to check the new API
  * ICMP module added options for logging and blocking ICMPv6
  * BasicFirewall rules improved to allow port ranges, bulk ports, and bulk IPs
  * Changed configuration location to AppData
  * Optimization of bandwidth counter
  * Added per-rule log notification for BasicFirewall and MACFilter modules
  * Updated tray icon

# 0.3.10.0 #
  * Module ordering and enabling/disabling is now preserved
  * Configurations now properly serialized
  * Improved pcap output performance
  * ICMP module GUI reworked
  * DDoS module GUI reworked
  * Now have the ability to define a DoS threshold; see the Help section for more info
  * IP Monitor module GUI reworked
  * Reworked rule addition GUIs
  * Ability to edit BasicFirewall and MACFilter rules now included
  * Selecting Help on a specific module now properly opens its respective tab
  * Improved performance of logging
  * BasicFirewall rules now take immediate effect
  * Ability to wipe ARP cache added
  * Basic IPv6 support
  * Fixed more bugs than thought possible
  * Improved clarity of module Help strings

# 0.3.6.1 #
  * Added ability to configure update check options
  * Added more getters/setters to protocols in Packets
  * Added GenerateChecksum method for TCPPacket

# 0.3.6.0 #
  * Added metadata to modules
  * Added help section
  * Added to the module interface
  * Bug fixes
  * Confirmed that VB.NET modules will work(Thanks to Broly)

# 0.3.5.0 #
  * Added multiple language support
  * Optimizations and bug fixes
  * Added more API ability, like create packets to send
  * New icon, thanks Broly
  * Error logging
  * Removed Maxmind

# 0.3.4.0 #
  * Added the ability to load user developed modules
  * Added the Mac Filter module
  * Added the Options tab, and the ability to turn off the pop up notifications

# 0.3.3.0 #
  * Improved the installer so it will uninstall previous versions

# 0.3.2.9 #
  * Allow users to enable/disable modules and change their processing order
  * Added the IP Monitor module