# get-mailboxes.ps1
#
# Script that retrieves a list of mailboxes through the Rackspace Email Api.  It serves to simplify the parameters that must be passed
# to get-resource, and parsing of the return XML.
# 
# Usage:
# PS > .\get-mailboxes.ps1 <acctNumber> <domain>

param
(
	[string] $acct,
	[string] $domain
)

if(!$acct) { throw "Please enter a valid account number (`$acct)" }
if(!$domain) { throw "Please enter a valid domain (`$domain)" }

(get-resource "customers/$acct/domains/$domain/ex/mailboxes").mailboxes.mailbox
