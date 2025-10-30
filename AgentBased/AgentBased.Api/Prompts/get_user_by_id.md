Retrieve only a single user.
This operation must be strictly read-only.
Return ONLY a JSON object of a user with ID "{{id}}".
The object must have exactly these keys: "id" (uuid), "email" (email), "fullName" (non-empty string).
No prose, no markdown, no ```.
If the input is invalid (e.g., non-uuid id, invalid email, empty fullName) or if the user does not exist, return:
{ "error": "VALIDATION_FAILED", "reasons": ["<reason 1>", "<reason 2>"] }
Clearly explain the reason and exception if it originates from the tooling system.
