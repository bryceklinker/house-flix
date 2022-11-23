#!/bin/bash

set -ex

MIGRATION_NAME="${1}"
SCRIPT_DIRECTORY=$(dirname "${0}")
DB_CONTEXT_TYPE="House.Flix.PostgreSQL.Common.PostgresHouseFlixStorage"
DB_PROJECT_PATH="${SCRIPT_DIRECTORY}/src/House.Flix.PostgreSQL"
STARTUP_PROJECT_PATH="${SCRIPT_DIRECTORY}/src/House.Flix.Service"

echo "Creating migration ${MIGRATION_NAME} for ${DB_CONTEXT_TYPE}"

dotnet ef migrations add "${MIGRATION_NAME}" \
  --startup-project "${STARTUP_PROJECT_PATH}" \
  --project "${DB_PROJECT_PATH}" \
  --context "${DB_CONTEXT_TYPE}"
  
echo "Created migration ${MIGRATION_NAME} for ${DB_CONTEXT_TYPE}"