# ECommerceTestTask
Test task #1 for "ООО НПП Новые Технологии Телекоммуникаций".
> Target platform .Net 7.0

> The application is self-contained and do not require any extra libraries.
> 
> The application has been tested on Windows 11.
>
> The application is not applicable with Linux-based OSs in a current version.

## Release version usage
1. Make sure you have an environmental variable named "ECOMMERCE_CONNECTION_STRING" added in your operating system to use SQL analysis.
   
> The variable should be added as User targeted for Windows OS.
> 
![изображение](https://github.com/AtFy/ECommerceTestTask/assets/75484528/17d814ad-2ee0-4bf0-b65c-a2ea16b676e2)

2. Connection string should look the following way.
```
user=<user_name>;password=<user_pwd>;database=<db_name>;server=<ip>;port=<port>;default command timeout=0;
```

3. The application can only work with .csv files while executing non-SQL analysis.

4. There is no possibility to execute a command with no date specified.
```Csharp
"1" // Wrong.
"1 | " // Wrong.

"1 | 01.02.2022-03.02.2022" // Correct.
```

5. Please, specify the full path executing non-SQL analysis.
```
C:\Users\Archie\Desktop\test.csv
```
