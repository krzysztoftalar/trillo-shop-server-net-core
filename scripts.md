# App Migrations
dotnet ef migrations add <name> -p Persistence -s WebUI

# Update Database
dotnet ef database update -p Persistence -s WebUI

# Stripe
stripe listen --forward-to https://localhost:5001/api/payments/webhook

# Dotnet user-secrets init
dotnet user-secrets init -p WebUI

# Dotnet user-secrets set
dotnet user-secrets  set "<name>:<name>" "<value>"