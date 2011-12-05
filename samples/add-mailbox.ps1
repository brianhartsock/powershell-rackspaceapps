# add-mailbox.ps1
#
# Script that adds a mailbox through the Rackspace Email Api.  It serves to simplify the parameters that must be passed
# to post-resource, and parsing of the return XML.
# 
# Usage:
# PS > .\add-mailbox.ps1 <acctNumber> <domain> <mailboxName> <password> <size - 2048 default> <displayname>
# PS > .\add-mailbox.ps1 <acctNumber> <domain> <mailboxName> <password> <size - 2048 default> <displayname> @{ firstName = <firstName>; ... }
param
(
	[string] $acct,
	[string] $domain,
	[string] $name,
	[string] $password,
	[int] $size,
	[string] $displayName,
	[hashtable] $otherInfo = @{}
)

if(!$acct) { throw "Please enter a valid account number (`$acct)" }
if(!$domain) { throw "Please enter a valid domain (`$domain)" }
if(!$name) { throw "Please enter a valid mailbox name (`$name)" }
if(!$password) { throw "Please enter a valid password (`$password)" }
if(!$size) { throw "Please enter a valid size (`$size)" }
if(!$displayName) { throw "Please enter a valid display name (`$displayName)" }

$otherInfo["password"] = $password
$otherInfo["size"] = $size
$otherInfo["displayName"] = $displayName

post-resource "/customers/$acct/domains/$domain/ex/mailboxes/$name" $otherInfo