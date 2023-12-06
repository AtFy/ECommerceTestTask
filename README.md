# ECommerceTestTask
DB and CSV analyzer with CLI. 
> Target platform .Net 7.0

> The application is self-contained and do not require any extra libraries.
> 
> The application has been tested on Windows 11.
>
> The application is not applicable with Linux-based OSs in a current version.
>
> There are several predefined features in this application. These features have been designed to work with [this specific dataset](https://www.kaggle.com/datasets/mkechinov/ecommerce-behavior-data-from-multi-category-store). They exist as a sample and the functionality may be changed within the specific needs. 

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

## Profiling info
> The application has been designed to work asynchronously and not to store all the data in memory. Each full analysis cycle ends up with GC.
>
> Peak memory usage detected is 21MB.
>
> Default memory usage detected is 2MB.

![1](https://github.com/AtFy/ECommerceTestTask/assets/75484528/ba7f5a00-7c9e-472a-adfe-8b5d01b9da25)

![2](https://github.com/AtFy/ECommerceTestTask/assets/75484528/419507d1-d0f3-4800-a7db-0623fdb8a68f)

![3](https://github.com/AtFy/ECommerceTestTask/assets/75484528/f03eb4e7-f585-468c-958b-3bea369423fb)
