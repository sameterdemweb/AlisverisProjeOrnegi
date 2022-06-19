
using AlisverisProje.Identity;
using AlisverisProje.Models.Guvenlik;
using AlisverisProje.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AlisverisProje.Controllers
{
    public class GuvenlikController : Controller
    {

        #region UserManager ve SignInManager tanımlarının tanımlanması.
        //Injection yapmak için değişkenlerimizi ilgili tiplerde tanımlıyoruz.
        UserManager<AppIdentityUser> _userManager;
        SignInManager<AppIdentityUser> _signInManager;

        public GuvenlikController(UserManager<AppIdentityUser> userManager, SignInManager<AppIdentityUser> signInManager)//ctor ile yapı denetim bloğu oluşturduk ve içerisinde injection edilen değerlimizi ilgili değişkenlere alıyoruz artık bu değişkenler üzerinden işlemlerimizi gerçekleştirebiliriz.
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        #endregion

        public IActionResult Index()
        {
            return View();
        }

        #region Giriş İşlemi Login

        public IActionResult Giris()
        {
            return View();
        }

        [HttpPost]// Post olarak gelen login methodumuzu tanımladık.
                  // "async"< ASenktron işlemler için tanımlanır. Clienti kullanan kullanıcıların işlemleri sıraya alınmaz ve çalıştığı anda ayrı ayrı işleme alınabilir.

        // "await" ASenktron olarak kullanılan methodlar await ile işaretlenir
        public async Task<IActionResult> Giris(LoginViewModel loginViewModel) // Login Sayfası için oluşturulacak 
        {
            if (!ModelState.IsValid) //Model geçerliliğini test ediyoruz. Bu kalıp ile modelin durumunun geçerli yani kullanıcının girdiği datanın geçerli olup olmadığını kontrol ediyoruz. Zorunlu olarak giriş yapılmış alanlar doldurulmuşmu gibi.
            {
                return View(loginViewModel);
            }

            var user = await _userManager.FindByNameAsync(loginViewModel.UserName); //Kullanıcı veritabanından gelip gelmediğini kontrol ediyoruz.

            if (user != null)//Kullanıcı bilgisi gelmişse
            {
                if (!await _userManager.IsEmailConfirmedAsync(user))
                {
                    ModelState.AddModelError(string.Empty, "Lütfen önce mail adresinizi onaylayın.");
                }
                else
                {
                    // Giriş işlemlerini ilgili model 'a gönderiyoruz.
                    /* 
                        1. Parametre KullanıcıAdı
                        2.Parametre Şifre
                        3. Parametre Beni Hatırla
                        4. Parametre Eğer lockout yani program.cs üzerinde tanımlanan yanlış giriş yapılırsa "5" adet kadar sistemden bir süreliğine uzaklaştır işlemi tanımlanmış ise. true yaparsak sisteme giremez ve lockout işlemi aktif olur. 
                    */
                    var sonuc = await _signInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, false, false);

                    if (sonuc.Succeeded)//Eğer kullanıcı var ise
                    {
                        return RedirectToAction("Index", "Musteri");
                    }


                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Kullanıcı Adı veya Şifre Hatalı.");
            }


            return View(loginViewModel);
        }


        #endregion

        #region Çıkış İşlemi Logout


        //Çıkış işlemini tanımlayalım.
        public async Task<IActionResult> Cikis()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region Üye Olma İşlemi Register

        public IActionResult UyeOl()
        {

            return View();
        }

        //Kayıt Viewinden Gönderim Sağlandığında aksiyon alacağı methodu tanımlıyoruz.

        [HttpPost]
        public async Task<IActionResult> UyeOl(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid) //Model geçerliliğini test ediyoruz. Bu kalıp ile modelin durumunun geçerli yani kullanıcının girdiği datanın geçerli olup olmadığını kontrol ediyoruz. Zorunlu olarak giriş yapılmış alanlar doldurulmuşmu gibi.
            {
                ModelState.AddModelError(string.Empty, "Lütfen doldurulması gereken alanları boş bırakmayınız..");
                return View(registerViewModel); //Model uymama durumunda view döndürüyoruz.
            }

            var kullanici = new AppIdentityUser
            { // Kullanıcı için bir nesne oluşturuyoruz. modelden gelen değeri tanımladık.
                UserName = registerViewModel.UserName,
                Email = registerViewModel.Email
            };
            //Kullanıcının bilgisini ve şifresini tanımladık.
            var sonuc = await _userManager.CreateAsync(kullanici, registerViewModel.Password);

            if (sonuc.Succeeded)//Eğer yapılan işlem başarılı olduysa yani kullanıcı bilgisi veritabanına kayıt edildiyse.
            {
                // Emailini doğrulaması için kullanıcıya bir kod üretiyoruz.
                var kodBilgisi = _userManager.GenerateEmailConfirmationTokenAsync(kullanici); //Doğrulama kodu oluşturduk.
                var DonusUrl = Url.Action("MailOnayla", "Guvenlik", new { userId = kullanici.Id, code = kodBilgisi.Result }); //Dönüş URL Tanımlıyoruz ilgili method, controler ve nesne istediği için ilgili nesneyi gönderdik. 


                #region Email Gönderme işlemi yapıLAcak ve DonusUrl gönderilecek.
                //Mail Gönderme! işlemi burada yapılacak.! // EMAİL SAĞLAYICI KULLANIMI GEREKLİ

                MailIslemleri.MailGonderme(DonusUrl, "Üye Aktivasyon");

                #endregion

                return RedirectToAction("Giris", "Guvenlik");

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Lütfen Parolanınızı Büyükharf, Küçükharf ve sayı olacak şekilde giriniz...");
            }

            return View(registerViewModel);
        }

        #endregion

        #region Mail Onaylama ConfirmMail

        public async Task<IActionResult> MailOnayla(string userId, string code)  //Gelecek olan parametrelerle mail onaylama işlemimizi tanımladık.
        {
            if (userId == null || code == null) // Defansing Coding yazımı.
            {
                return RedirectToAction("Index", "Home"); //Boş geldiyse işlem yapmadan göndermemiz gerekli.
            }

            var user = await _userManager.FindByIdAsync(userId); // Gelen Id ile veritabanınmızda kullanıcı varmı diye baktık.

            if (user == null) // Böyle bir kullanıcı yok ise!
            {
                throw new ApplicationException("Kullanıcı Sistemde Bulunamadı."); //View'e bir hata mesajı fırlatır :)
            }

            var result = await _userManager.ConfirmEmailAsync(user, code); //eğer kullanıcı bulunduysa ve code eşleşiyor ise. true değer döndürür.

            if (result.Succeeded)//Yapılan işlem başarılı ise 
            {
                return View("MailDogrulamaYapildi"); //MailDogrulamaYapildi Viewine döndürür.
            }

            return RedirectToAction("Index", "Home");//  Eğer yukarıdakilerden hiçbiri geçerli değil ise bu alan çalışır.
        }

        #endregion

        #region Şifremi unuttum İşlemi ForgetPassword.

        public IActionResult SifremiUnuttum()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SifremiUnuttum(string mail)
        {
            if (string.IsNullOrEmpty(mail)) //Gelen parametre boşmu diye bakıyoruz.
            {
                return View();
            }

            var kullanici = await _userManager.FindByEmailAsync(mail);// Gelen mail adresine uygun üyeliklerde mail varmı veritabanında diye kontrol ediyoruz varsa kullanıcı bilgisini alıyoruz.

            if (kullanici == null)//Kullanıcı gelmemiş ise view döndürüyoruz.
            {
                return View();
            }

            var DogrulamaKodu = await _userManager.GeneratePasswordResetTokenAsync(kullanici); //Şifre güncelleme işlemi için bir token oluşturduk.
            var DonusUrl = Url.Action("SifreSifirlamaYap", "Guvenlik", new { userId = kullanici.Id, code = DogrulamaKodu }); //Dönüş URL Tanımlıyoruz ilgili method, controler ve nesne istediği için ilgili nesneyi gönderdik. 

            #region Email Gönderme işlemi yapıLAcak ve DonusUrl gönderilecek.
            //Mail Gönderme Kodları  // EMAİL SAĞLAYICI KULLANIMI GEREKLİ

            MailIslemleri.MailGonderme(DonusUrl, "Şifre Sıfırlama");

            #endregion


            return RedirectToAction("SifreSifirlamaMaileGonderildi"); //Kullanıcı işlemini tamamladıysa bizim kullanıcıya git e maile bak ve linke tıklayıp şifreni güncelle dememiz gerekmekte.
        }

        public IActionResult SifreSifirlamaMaileGonderildi()
        {
            return View(); //Bu viewde sıfırlama gönderildi lütfen mailinizi kontrol edin sayfasına göndereceğiz.
        }


        public IActionResult SifrenizGuncellendi()
        {
            return View(); //Bu viewde sıfırlama gönderildi lütfen mailinizi kontrol edin sayfasına göndereceğiz.
        }

        public IActionResult SifreSifirlamaYap(string userId, string code)
        {
            if (userId == null && code == null)
            {
                throw new ApplicationException("Doğrulama Kodu veya Kullanıcı Bilgisi Doğru Değil");
            }

            var model = new ResetPasswordViewModel { Code = code };

            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> SifreSifirlamaYap(ResetPasswordViewModel resetPasswordViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Lütfen doldurulması gereken alanları boş bırakmayınız..");
                return View(resetPasswordViewModel);//Kullanıcı girmesi gereken yerleri girmemiş
            }

            var user = await _userManager.FindByEmailAsync(resetPasswordViewModel.Email);   //gelen maile uygun kullanıcı var mı?
            if (user == null)
            {
                throw new ApplicationException("Kullanıcı Bulunamadı");
            }

            var result = await _userManager.ResetPasswordAsync(user, resetPasswordViewModel.Code, resetPasswordViewModel.Password); // Bulunan kullanıcı bilgisi, Resetleme Kodu ve Yeni şifre gönderilir.

            if (result.Succeeded) //Yenişifre olarak veritabanına eklenme işlemi bittiyse eğer
            {
                return RedirectToAction("SifrenizGuncellendi");
            }

            return View();

        }

        public IActionResult MailDogrulamaYapildi()
        {
            return View();//şifreniz güncellendi viewine gönder
        }

        #endregion

        #region Yetki Yok Bilgilendirme
        public IActionResult ErisimYetkinizYok()
        {
            return View();
        }
        #endregion

    }
}
