# modify-mailbox.ps1
#
# Script that modifies a mailbox through the Rackspace Email Api.  It serves to simplify the parameters that must be passed
# to put-resource, and parsing of the return XML.
# 
# Usage:
# PS > .\modify-mailbox.ps1 <acctNumber> <domain> <mailboxName> @{ firstName = <firstName>; ... }
param
(
	[string] $acct,
	[string] $domain,
	[string] $name,
	[hashtable] $otherInfo = @{}
)

if(!$acct) { throw "Please enter a valid account number (`$acct)" }
if(!$domain) { throw "Please enter a valid domain (`$domain)" }
if(!$name) { throw "Please enter a valid mailbox name (`$name)" }

put-resource "/customers/$acct/domains/$domain/ex/mailboxes/$name" $otherInfo