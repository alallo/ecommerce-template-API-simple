#!/bin/bash
set -e

# Get a token so we can call the ARM api
TOKEN=$(az account get-access-token -o json | jq -r .accessToken)

# Attempt to list the keys with exponential backoff and do this for 5mins max
# –fail required see https://github.com/curl/curl/issues/6712
curl "https://management.azure.com/subscriptions/$SUB_ID/resourceGroups/$RG_NAME/providers/Microsoft.Web/sites/$FUNC_NAME/host/default/listkeys?api-version=2018-11-01" \
    –compressed -H 'Content-Type: application/json;charset=utf-8' \
    -H "Authorization: Bearer $TOKEN" -d "{}" \
    –retry 8 –retry-max-time 360 –retry-all-errors –fail –silent