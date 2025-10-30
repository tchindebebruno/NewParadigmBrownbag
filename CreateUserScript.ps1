Invoke-RestMethod -Uri "http://localhost:5110/users" `
    -Method Post `
    -Headers @{ "Content-Type" = "application/json" } `
    -Body '{"email": "Ramane@example.com", "fullName": "Raman"}'
