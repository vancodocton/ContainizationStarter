var client = new HttpClient();

bool isHealthy = false;
try
{
    var response = await client.GetAsync(args[0]);
    if (response.IsSuccessStatusCode)
        isHealthy = true;
}
catch
{
    isHealthy = false;
}

if (isHealthy)
    return 0;
else
    return 1;