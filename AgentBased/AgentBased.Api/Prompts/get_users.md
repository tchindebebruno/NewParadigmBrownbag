Retrieve the list of users. Return ONLY a JSON array of users sorted by fullName in ascending order.
* To retrieve users, you MUST call get_users(). Never call create_user for this operation.
* Each item must have exactly these keys: "id" (uuid), "email" (email), "fullName" (non-empty string).
* Always return $s users, skipping the first $offset users.
* No prose, no markdown, no ```.

Before executing, you must first calculate the values of $s and $offset based on the input parameters, where page equals {{page}} and pageSize equals {{pageSize}}.
* First, determine p as the maximum between 1 and the provided page value (or 1 if page is null).
* Then, compute s by clamping pageSize (or 20 if null) to stay within the range 1 to 200.
* Finally, calculate offset as (p - 1) * s.

These computed values must then be used for the subsequent operation.

If an entry is invalid (e.g., non-uuid id, invalid email, empty fullName), exclude the invalid data.
If the entire list is invalid, return:
{ "error": "VALIDATION_FAILED", "reasons": ["<reason 1>", "<reason 2>"] }
