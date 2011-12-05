# add-mailbox.ps1
#
# Script that removes a mailbox through the Rackspace Email Api.  It serves to simplify the parameters that must be passed
# to remove-resource, and parsing of the return XML.  It also uses confirmations to make sure the deletion is desired.
# 
# Usage:
# PS > .\remove-mailbox.ps1 <acctNumber> <domain> <mailboxName>

param
(
	[string] $acct,
	[string] $domain,
	[string] $name
)

if(!$acct) { throw "Please enter a valid account number (`$acct)" }
if(!$domain) { throw "Please enter a valid domain (`$domain)" }
if(!$name) { throw "Please enter a valid mailbox name (`$name)" }

remove-resource "/customers/$acct/domains/$domain/ex/mailboxes/$name" -confirm