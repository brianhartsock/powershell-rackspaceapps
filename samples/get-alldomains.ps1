# get-alldomains.ps1
#
# Script that retrieves a list of domains through the Rackspace Email Api.  It serves to simplify the parameters that must be passed
# to get-resource, and parsing of the return XML.  It also uses the special 'all' customer type so that iterating through all 
# customers is not required.
# 
# Usage:
# PS > .\get-alldomains.ps1

(get-resource "customers/all/domains").domains.domain
