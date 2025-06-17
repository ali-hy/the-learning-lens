using Tester;

Console.WriteLine("Initializing API...");
Api api = await Api.Initialize(new()
{
    Email="ali.haitham.ymy@gmail.com",
    Password="changeMe@123"
});
Console.WriteLine("Initialized API");

Console.WriteLine($"Auth Key: {api.Auth.Info?.AccessToken}");
Console.WriteLine($"FirstName: {(await api.Auth.GetUserInfo()).FirstName}");

Console.Write("Please press any key...");
Console.ReadKey();