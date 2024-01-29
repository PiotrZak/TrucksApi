# Wait to be sure that SQL Server came up
sleep 90s

echo "Running Create Database Script..."

# Install mssql-tools package (if not already installed)
# apt-get update
# apt-get install -y mssql-tools


# Run the setup script to create the DB and the schema in the DB
# Note: make sure that your password matches what is in the Dockerfile
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P password123! -d master -i create-database.sql