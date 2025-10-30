Create a new user with fullName "{{fullName}}" and email "{{email}}".
Validate that both fields are present and that the email is in a valid format. 

INPUT VARIABLES
     - fullName  (string)
     - email     (string)

VALIDATION
   - If fullName= "{{fullName}}" is missing or empty -> return error JSON.
   - If Email= "{{email}}" is missing or does not match a simple email regex -> return error JSON.

OUTPUT
   - Return only JSON (No extra prose, no markdown, no ```.). On success:
     {
       "id": "<generated-id>",
       "fullName": "{{fullName}}",
       "email": "{{email}}"
     }
   - On validation failure -> return error JSON.
 Error JSON
 - Here is the schema of error JSON (No extra prose, no markdown, no ```): { "error": "VALIDATION_FAILED", "reasons": ["<reason 1>", "<reason 2>"] }
 - Provide clear reasons for validation failures.

