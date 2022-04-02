using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TwitterClone_Carlos2036114_Ines2036314.Models;
using TwitterClone_Carlos2036114_Ines2036314.Services;

namespace TwitterClone_Carlos2036114_Ines2036314.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration config;
        private readonly IJWTService tokenService;
        private readonly IUserService userService;
        private readonly IPostService postService;
        public readonly ICommentService commentService;
        private readonly IWebHostEnvironment hostEnvironment;

        public HomeController(ILogger<HomeController> logger, IJWTService tokenService, IUserService userService, IPostService postService, ICommentService commentService, IWebHostEnvironment hostEnvironment)
        {
            this._logger = logger;
            this.userService = userService;
            this.postService = postService;
            this.commentService = commentService;
            this.hostEnvironment = hostEnvironment;
            this.tokenService = tokenService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            string token = HttpContext.Session.GetString("Token");
            var id = tokenService.GetJWTTokenClaim(token);
            //var first = new FirstModel { FProperty1 = "A" };
            var user = userService.GetByUserId(int.Parse(id));

            var posts = postService.GetAllPosts();
            //Posts_ allPosts = new Posts_();
            //allPosts.PostsList = posts;

            var tupleModel = new Tuple<User, Posts_>(user, new Posts_ { PostsList = posts });
            return View("Index", tupleModel);
        }


        public IActionResult Privacy()
        {
            return View();
        }

  
        public IActionResult Edit(int id)                   // carrega a view com o post seleccionado
        {
            var post = postService.GetPost(id);
            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Post post)
        {
            /*if (ModelState.IsValid)
            {*/
                var newPost = postService.EditPost(post, post.postId);
                if (newPost is not null)
                    return RedirectToAction(nameof(Index)); //redirect para a view index
                else
                    return RedirectToAction(nameof(Error)); //redirect para a view de erro
            /*}
            else
            {
                return RedirectToAction(nameof(Error));
            }*/
        }



        [Route("Home/Profile")]
        public IActionResult Profile()
        {

            string token = HttpContext.Session.GetString("Token");
            var id = tokenService.GetJWTTokenClaim(token);
            //var first = new FirstModel { FProperty1 = "A" };
            var user = userService.GetByUserId(int.Parse(id));

            var posts = postService.GetPostByUser(int.Parse(id));
            var uvm = new UserViewModel { Posts = posts }; 

            var tupleData = new Tuple<User, IEnumerable<Post>>(user, posts);
            return View(tupleData);

        }

        public IActionResult Login()
        {
            return View("Login");
        }


        [AllowAnonymous]
        [Route("Home/Login")]
        [HttpPost]
        public IActionResult Login(User userModel)
        {
            if (string.IsNullOrEmpty(userModel.userName) || string.IsNullOrEmpty(userModel.Password))
            {
                return (RedirectToAction("Error"));
            }

            var user = userService.GetByUserNameAndPass(userModel.userName, userModel.Password);
            var validUser = new User { userName = user.userName, userId = user.userId, Password = user.Password, Email = user.Email };

            if (validUser != null)
            {
                string generatedToken = tokenService.GenerateToken(
                    "thisismysecretkey",
                    "djardim",
                    "https:localhost:7071/",
                    validUser);

                if (generatedToken != null)
                {
                    HttpContext.Session.SetString("Token", generatedToken);
                    return RedirectToAction("Index", validUser);
                    //return ViewBag("Index", generatedToken);
                }
                else
                {
                    return (RedirectToAction("Error"));
                }
            }
            else
            {
                return (RedirectToAction("Error"));
            }
        }

        //[AllowAnonymous]
        //[HttpPost]
        //public IActionResult Login(User user_)
        //{
        //     if (string.IsNullOrEmpty(user_.Email) || string.IsNullOrEmpty(user_.Password))
        //     {
        //         return (RedirectToAction("Error"));
        //     }
        //    // alterar a funcao get user by email e acrescentar a verificaçao da passe


        //    var user = userService.GetByUserByEmail("teste@teste.pt");

        //     var validUser = new User {userId = user.userId, Password = user.Password, Email = user.Email };

        //     if (validUser != null)
        //     {
        //         string generatedToken = tokenService.GenerateToken(
        //             config["Jwt:Key"].ToString(),
        //             config["Jwt:Issuer"].ToString(),
        //             config["Jwt:Audience"].ToString(),
        //         validUser);

        //         if (generatedToken != null)
        //         {
        //             HttpContext.Session.SetString("Token", generatedToken);
        //             return RedirectToAction("Profile", validUser);
        //         }
        //         else
        //         {
        //             return (RedirectToAction("Error"));
        //         }
        //     }
        //     else
        //     {
        //         return (RedirectToAction("Error"));
        //     }

        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }




        [AllowAnonymous]
        [HttpPost]

        public async Task<IActionResult> Registo(User user, IFormFile file)
        {
            var userExists = userService.GetByUserName(user.userName);


            if (userExists == null)
            {

                string path = Path.Combine(this.hostEnvironment.WebRootPath, "staticImage");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string fileName = Path.GetFileName(file.FileName);



                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    file.CopyTo(stream);

                    // return RedirectToAction("Image", new User { profileImage = file.FileName });

                    user.profileImage = fileName;
                    var newUser = userService.CreateUser(user);
                    if (newUser is not null)
                        return RedirectToAction(nameof(Login));
                    else
                        return RedirectToAction(nameof(SignUp));


                }

                

            }
            else {
                return StatusCode(StatusCodes.Status500InternalServerError, "User already exists!");
            }


            
        }

        public IActionResult Logout()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult LogoutUser()
        {
            HttpContext.Session.Remove("Token");
            return (RedirectToAction("Login"));
        }

        public IActionResult SignUp()
        {

            string token = HttpContext.Session.GetString("Token");
            var id = tokenService.GetJWTTokenClaim(token);

            ViewBag.Id = id;

            return View();
        }


        //[HttpGet]
        //public IActionResult Profile(User user)
        //{
        //    string token = HttpContext.Session.GetString("Token");

        //    if (token == null)
        //    {
        //        return (RedirectToAction("Index"));
        //    }

        //    if (!tokenService.IsTokenValid(
        //        config["Jwt:Key"].ToString(),
        //        config["Jwt:Issuer"].ToString(),
        //        config["Jwt:Audience"].ToString(),
        //        token))
        //    {
        //        return (RedirectToAction("Index"));
        //    }

        //    ViewBag.Token = token;
        //    return View(user);
        //}


        public IActionResult Image()
        {

            string token = HttpContext.Session.GetString("Token");
            var id = tokenService.GetJWTTokenClaim(token);
            //var first = new FirstModel { FProperty1 = "A" };
            var user = userService.GetByUserId(int.Parse(id));


            return View(user);
        }

        public IActionResult UploadImage(User user)
        {
            return View("UploadImage", user);
        }

        [HttpPost]
        public IActionResult UploadImage(IFormFile file)
        {
            string path = Path.Combine(this.hostEnvironment.WebRootPath, "staticImage");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string fileName = Path.GetFileName(file.FileName);


            using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                file.CopyTo(stream);
                return RedirectToAction("Image", new User { profileImage = file.FileName });
            }

            //return RedirectToAction("Error");
        }
    }
}
