# Print a message to the console
echo "Starting initialization script and SQL Server..."

# Run the initialization script in the background
/usr/src/app/run-initialization.sh &

# Run Microsoft SQL Server
/opt/mssql/bin/sqlservr