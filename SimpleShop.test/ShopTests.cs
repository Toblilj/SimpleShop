using Xunit;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;
using SimpleShop.Core.Entites;

namespace SimpleShop;
// using SimpleShop.Core.Entites;
// using System.Net.Http.Headers;
// using SimpleShop;

public class UnitTest1
{
    [Fact]
    public void Test_Validate_Correct_Price_IsReturned()
    {
        //Arrange
        //här påbörjar vi vårt case och vi börjar med att vi vill skapa våra preconditions. 
        // först måste vi anropa det vi ska testa så vi skapar ett objekt av ShopService.cs

        var shopService = new ShopService();
        //vi ska testa CalculatePrice och för att göra det behöver kolla på vad den vill ha,den vill ha 2 saker
        //product produc och int quantity, så vi måste skapa båda för att skapa båda så behöver vi titta på konstruktorn product
        //för att skapa product kollar vi på konstruktorn i product.cs
        //deklarar våra variabler- här kollar vi på paramterarna på konstruktorn 
        //vi använder även som 100m- m är för att denna metoden vi testar hanterar decimaler.
        //detta testet kräver enligt konstruktorn i product.cs     
        //  att vi har 
        //först en id som är en int dvs "1", sedan en sträng dvs "product", sedan en decimal dvs "100m"
        //och sist en int till som är lagersaldo dvs "2"
        var product= new Product(1,"product",100m,2);
        var quantity= 2;
        
        //act
        // Jag kör metoden jag testar (CalculatePrice) på mitt objekt shopService,
        // med produkten och antalet jag skapade i Arrange.
        // Svaret sparas i result, så att jag kan kontrollera det i Assert
        var result=shopService.CalculatePrice(product,quantity);
        
        //assert
        //här vill vi jämföra med test värdet vi har satt mot det vi får när metoden körs.
        Assert.Equal(200,result);



    }


    [Fact]
    public void Test_OneProductIs49_WhatIsPriceOf5()
    {
        // arrange
    //skapar objektet
    var shopService=new ShopService();
    var product=new Product(2,"Product",49m,5);
    var quantity=5;
        // act
    var result=shopService.CalculatePrice(product,quantity);
        // assert
        Assert.Equal(245,result);
    }



    //vi använder theory för att köra samma test fler gånger men med olika testdata
    //InlineData är testdatan för alla cases, i detta fallet 3.
    //Iomed vi kör med theory så måste vi även sätta parametrar i  vår metod,därav 
    //decimal price och bool expected
    //här blir paramterarna i metoden dina test  variabler, därav behövs inte inValidValue
    [Theory]
    [InlineData(100,2,200)]
    [InlineData(210,1,210)]
    [InlineData(200,0,0)]
    public void Test_IsValidPrice_NegativePrice_ReturnFalse(decimal price,int quantity,decimal expected)
    {
        //arrange
        var shopService=new ShopService();
        //i denna variabeln sätter vi värdena för testet- Baserat på vår InLineData
        //t.e.x i detta fallet vill vi skriva in price istället för sätta ett pris.
      var product=new Product(1,"product",price,10);
        //act
        var result=shopService.CalculatePrice(product,quantity);
        //assert
        //här vill vi jämföra med våran expected ifrån InLineData
        Assert.Equal(expected,result);
    }

    //caset ska handla om vad händer om det är ett negativt tal?
    [Fact(Skip ="Egen lösning för att avrosta inför TDD")]
    public void Test_WhatifNegative()
    {
        //arrange
        var shopService=new ShopService();
        var product=new Product(1,"product",10m,5);
        var quantity=-5; //här är sätter vi en extra variabel för antal eftersom vi ska testa 
        //om vad som händer när värdet är mindre än 0. dvs negativt. 
    
      
        //act + assert
        // Negativt antal är ogiltig indata. Förväntar mig ArgumentException.
// Testet failar tills validering läggs till i CalculatePrice.
        Assert.Throws<ArgumentException>(() => shopService.CalculatePrice(product,quantity));
    }

    [Fact]
    public void Test_WhatIfNegative_AssignmentSolution ()
    {
        var shopService=new ShopService();
        var product=new Product(1,"product",10m,5);
        var quantity=-5;

        var result=shopService.CalculatePrice(product,quantity);

        Assert.Equal(-50m,result);
    }

    [Theory]

[InlineData (100,true)]
[InlineData(1,true)]
[InlineData(0,false)]
[InlineData(-10,false)]
[InlineData(0.01,true)]
    public void Test_BoundaryValue_OfInvalidPrice(decimal price, bool expected)
    {
        var shopService=new ShopService();
        

        var result=shopService.IsValidPrice(price);

        Assert.Equal(expected,result);
    }







}
