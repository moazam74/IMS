# Database Scripts

This folder contains all database-related scripts for the Inventory Management System.

## Folder Structure

- **StoredProcedures/**: Contains all stored procedures
  - `GetSalesByCategory.sql`: Retrieves sales data grouped by category

## Usage

1. All scripts are idempotent and can be run multiple times safely
2. Each stored procedure includes:
   - Documentation header
   - Drop statement if exists
   - Create statement
   - Example usage

## Naming Conventions

- Stored Procedures: `PascalCase`
- File Names: Match the stored procedure name
- Parameters: `@PascalCase`

## Best Practices

1. Always include documentation headers
2. Use SET NOCOUNT ON in stored procedures
3. Include example usage comments
4. Make scripts idempotent with IF EXISTS checks
5. Use proper indentation and formatting

## Adding New Scripts

1. Create new script in appropriate subfolder
2. Follow naming conventions
3. Include documentation
4. Add reference to this README if adding new folder 