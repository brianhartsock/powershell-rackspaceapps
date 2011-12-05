# get-customers.ps1
#
# Script that retrieves a list of customers through the Rackspace Email Api.  It serves to simplify the parameters that must be passed
# to get-resource.
# 
# Usage:
# PS > .\get-customers.ps1

(get-resource "customers").customers.customer