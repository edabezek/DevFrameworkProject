# DevFramework

## Katmanlar 

**Data Access  :** Veritabanında insert-update-delete sorgularını çalıştıracağımız katman. Orm implementasyonları bu katmanda olur.Biz EF ve NHibernate ekledik. Abstract klasörü, diğer katmanların veriye erişim için kullanacağı klasör.Böylece iş katmanı EF yada NHibernate bağlı olmayacak.

**Business  :** Bu katmanda projeyi ilgilendiren iş süreçlerini kodlarız.Örneğin bir kişinin ehliyete ihtiyacı var, biz bu kişiye ehliyet verelim mi diye burada kodlarız.

**Core :** Framework katmanında ; Loglama,Cacheleme,Transaction yönetimi,Performans yönetimi,Validasyon yönetimi,Rol bazlı güvenlik ve diğer araçlarımızı ,veritabanı Orm entegrasyonlarını burada yaparız.

**Entities  :** Burada normal nesnelerin yanı sıra joinli tabloları tutarız.

**Mapping :** Veritabanı ile nesnelerimizin bağlantısını kurar,ilişkilendirilmesini sağlar.Türkçe veritabanı kullanırsak bunu yapmak zorundayız.

**Test  :** Yaptığımız işlemlerin çalışıp çalışmadığını kontrol etmek için Unit Test kullanacağız.(Validation test için test framework kulladık -Moq framework data oluşturmamızı sağlayacak.)

## Teknolojiler 

**ILSpy	:** .NET assembly decompiler , Businnes'deki Aspect'lerin kodlarımızda nasıl bir değişiklik yaptığına bakmak için kullanacağız.

**Ninject  :** DataAccess Katmanına erişmek için Businnes'in constructor blogunda IProductDal injection yaptık Yani Product Manager'ı bize verilen bir IProductDal türündeki nesneye göre gerçekleştirdik.Solid'e göre hiçbir katmanda diğer katmanın instance’ını çağıramayız( new leyemeyiz). Bu yüzden Business katmanda Dependency Injection konfigürasyonu yapacağız.Bunun için DI container’ı olan Ninject’i kullanacağız. İnstance Factory bize business Module döndürür,orada da neyi nasıl çözeceğini belittik.Buraya AutoMapper de eklersek onu da yönlendirmiş olacağız.

**Transaction  :** Arka arkaya 3 işlem yaptık diyelim, ilk ikisi başarılı sonuncusu başarısız oldu.Böyle bir durumda karar vermemiz gerekiyor ,önceki işlemleri geri mi alacağız yoksa böyle devam edeceğiz.Bunun için transaction oluşturulur.

**Cache	:** Framework katmanına (Core) ekleyeceğiz. Bir data istenildiğinde cache’ye eklenecek. Bunun için birçok yöntem kullanılabilir, biz Memory Cache kullanacağız( bundan sonra o datayı kullanan kullanıcılar eğer cache'de varsa datayı çekebilecek).Output Caching’de, eğer bir data parametreleriyle beraber cachelenirse ,aynı parametrelerle aynı data çağrıldığında tekrardan veri çekmeyecek cache’deki data kullanılacak(Output ve Data Caching).
MemoryCache ,microsoft .net framework içinde default olarak gelen caching yapısıdır.Bu altyapı datayı uygulama sunucusunun belleğinde tutacak ve uygulama sunucusunun belleğindeki dataya göre tüketip işlem yapabilecek.Uygulama sunucusu, tek ve bir ISS üzerinden yayın yapıyorsak memory cache performanslı olarak işimizi görecektir. MemCache, RedisCache vs de kullanılabilir.(Load Balancing,farklı uygulama sunucuları kullanıyoruz veya cache'i ayrı sunucuda tutmak istediğimizde kullanabileceğimiz yapıdır.)

**Log  :** Framework katmanında konfigürasyonunu Log4Net ile yaptık. Product Manager'daki Update metodunu loglamak istiyoruz bunun için kim hangi metodu,hangi parametrelerle çalıştırdı bilgisini tutmamız gerekir.Dolayısıyla log'u tutacak datayı yönetebileceğimiz bir nesneye ihtiyacımız var. Bu da LogParameter olacak. Loglama veritabanına,metin dosyasına,xml ,eventviewer,console vs yazılabilir.Biz Json formatında tutacağız.Json formatına bir nesneyi çevirebilmemiz için onu Serializable hale getirmeliyiz. Bunu Layouts/JsonLayout'da yapacağız(ayrıca bunun için classlara Serializable ekliyoruz).
Loglar ihtiyaca göre nerede çalışacağı değişir.Örneğin metodun başında kim ne zaman hangi metodu çağırdı gibi birşey istersek o zaman OnEntry'le yapabiliriz.Metodun sonunda çağırabiliriz ve hata verdiğinde de.Log4Net için config dosyası oluşturmalıyız,bunu MVC Web UI içine oluşturup,web config içinde bunu kullanacağımızı belirttik.
Bütün Manager'lar için performans ,loglama veya exception yapmak istersek, Assembly seviyesinde ekleme yapmamız gerek (Bu class business'da properties altında).

**Performans Yönetimi :** Yazılım tarafındaki yavaşlıkları tespit etmek adına uygulamada herşey mi yavaş çalışıyor yoksa belli başlı metotlar mı yavaş çalışıyor,bunu tespit etmemiz gerekiyor.Bunun için performansı takip edebilmeliyiz.Metotların başına ve sonuna bir kronometre koyduk, bizim verdiğimiz metrikten daha yüksekse o zaman uyarı vermesini sağladık , bu yapıyı Performance Aspect class’ı ile yapacağız.

**Rol Bazlı Kullanıcı Yönetimi :** Postsharp kullanıp, Asp.Net Mvc'de de Identity yapısını da kullanarak Claim bazlı Auth yapacağız.İlk önce veritabanında User,User Roles ve Roles tabloları oluşturduk(tabloların ilişkilerinin belirtilmemesi halinde veri kaçağı oluşur,koyulması gerekir).Bir rolun olup olmadığını Aspect oluşturup kontrol edeceğiz. Örneğin GetAll metodunu sadece belli role sahip kişilerin görmesini istiyoruz.Bunun için metot başında Secured Operation kullandık.Identity için kendi configurasyonumuzu, Cross Cutting Concern’de , Security altına, Identity nesnesi tanımlayarak yapıyoruz.(Form Authentication yöntemi, kullanıcı bilgilerinin veri tabanında tutulduğu ve sorgu ile alındığı sistemlerdir.) (Session'da rolleri tutamayız.) Kendi Authentication Helper'ımızı yazdık(aslında Form Auth kullanıp ezdik).Web klasörü ,web arayüzleri için(client olarak web) kullanılacak bir durum olduğundan buraya ekledik.Authentication Helper kullanıcı bilgilerini alır şifreler ve cookie’ye basar.Cookie’yi okuyup kişinin kim olduğu vs bilgilerini almak için Authentication Helper'ın Create Auth Cookie metodu ile oluşturulan Cookie'nin ,uygulama boyunca kullanılabilecek bir nesneye (Identity’ye),çevrilmesi gerekiyor ki kontrollerimizi  yapabilelim. FormAuthentication ticket’ını bir Identity'e çevirmek için Core katmanına Web klasörüne Security Utilities class oluşturduk.

**Web Api Katmanı :** Api'lerimizi yazdığımız katman,api'lerin dışında kod yazmamalıyız. İşlemleri business üzerinden yürütmeliyiz( Arayüzün yerini api alır).Web Api'de kişi direkt controller’a istekte bulunuyor, bir modal-view-controller olmuyor, dolayısıyla security aspect buradan geçmiyor. Bunun için, bir AuthHandler yazıp ,biri bize istekte bulunduğu zaman mesajı yakalamak ve bunun sonucunda kişiyi (eğer bize kullanıcı adı ve şifre göndermişse) authenticate olmasını sağlayacağız. MessageHandler'ı istek kontrolü için yazdık.WepApi projesinde App_Start, WebApiConfig sınıfının içine konfigürasyonu yazıyoruz.Authentication bilgilerini veritabanından almak için IUserService'i AuthHandler'a enjekte ediyoruz,bunu InstanceFactory ile yaptık.

**AutoMapper :** Dependency Injection kullanarak verdik.Serileştirmeyi Business'da AutoMapper ile yaptık.

**WCF  :** Servis odaklı bir mimaride (SOA) ,client server mimarileri baz alınır.İki pc olur biri asp.net diğeri wcf-service'i çalıştırır.Asp.net tamamen servis'ten desteklenir(dll'den businnes'e gitmeyip service üzerinden çalışır). Tüm mimariyi servis odaklı mı çalışmamız lazım yoksa bazı servisleri mi açmak istiyoruz,bunların yaklaşımı birbirinden farklıdır.Bizim bir asp.net projemiz var ,service'ler başka bir server'dan bize hizmet edecek,bu şekilde kullandık. IProductService'in sevis olabilmesi için ServiceContract ve OperationContract özellikleri belirtilmelidir.Bunları belirtmek alternatif olarak yerine Wcf katmanında App_Code klasörü altına ServiceContracts interface IProductDetailService 
eklenebilir.Yada business katmanına ServiceContract altına yapılıp ProductDetailService Wcf'de kullanılabilir.Service.svc dosyasını ProductService.svc diye değiştirdik.Bu bizim yayın dosyamız.( Note : service.svc dosyası açıkken çalıştırırsak Acf test client editor gelir.) WebApi'de yada Mvc'de yaptığımız gibi connectionstring ekledik. Mvc tarafında Wcf servisini çağırdık(Normalde Wcf projesini çağırmanın en basit yolu,Mvc katmanına tıklayıp Add>Service Reference ile Proxy oluşturup eklenir.Bizimki client-server mimarisi olduğu için başka yöntem kullanacağız). Biz ChannelFactory ile runtime'da bir proxy oluşturup,onun üzerinden ilerleyeceğiz.Wcf Proxy ile Core katmanında proxy üreteceğiz, bunu Mvc'de çağıracağız.Mvc katmanında web.config dosyasına key olarak ekliyoruz. Ayrıca ChannelFactory'i üretecek BusinnesModule yerine ServiceModule yazacağız.Ve bunu  Mvc'de çalıştırmak için global.asax dosyasında factory kısmında belirttik.

**Angular   :** Mvc projesinin üstüne angular implemente edeceğiz.Bunun için öncelikle node kuracağız.TsScripts ,angular kodumuz için.Script, javascript kodumuz için.TypeScript kodunu JavaScript'e Gulp ile çevireceğiz.Package.json dosyasını Angular projesine ekliyoruz.Buraya gulp paketlerini de eklemeliyiz. Gulp, yazdığımız scriptlerin bir yerden bir yere taşınmasını ,değişiklik olduğu zaman build edilip atılmasını sağlayacak.Tsconfig.json , typescript konfigürasyonlarını içerir.Buraya outDir eklenir.

## Kullanılan Paketler  
--DevFramework.Core => Entity Framework 6.1.3,NHibernate 5.3.13,Fluent Validation 11.2.2,PostSharp 4.2.17,Log4Net 2.21.0,AutoMapper.6.1.1

--DevFramework.DataAccess => Entityframework 6.1.3 ,NHibernate 5.3.13,FluentNHibernate 3.1.0

--DevFramework.DataAccess.Test => Entity Framework 6.1.3 

--DevFramework.Businnes => Fluent Validation 11.2.2 ,PostSharp 4.2.17,Ninject 3.3.6,AutoMapper.6.1.1

--DevFramework.Businnes.Test =>PostSharp 4.2.17,MOQ 4.18.2 ,Fluent Validation 11.2.2 

--DevFramework.MvcWebUI =>Entity Framework 6.1.3, PostSharp 4.2.17

--DevFramework.WebApi => WebApiContrib.IoC.Ninject 0.9.3 ,Ninject 3.3.6,Ninject.MVC5.3.3.0,Ninject.Web.Common.3.3.2,
 Ninject.Web.Common 3.3.2,Entity Framework 6.1.3 
 
--DevFramework.WcfService => EntityFramework.6.1.3

--Pc için => PostSharp 4.2.17 
