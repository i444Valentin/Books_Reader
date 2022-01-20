using BooksReader.App;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WindowChrome.Demo.Pages;

namespace WindowChrome.Demo
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void PrevPageButton_Click(object sender, RoutedEventArgs e)
        {
            //App.ParentWindowRef.ParentFrame.Navigate(App.WelcomeRef);
            NavigationService.GoBack();
        }



        private void SingupButton_Click(object sender, RoutedEventArgs e)
        {
            User user = new User
            {
                Firstname = ((TextBox)FindName("firstnameTextBox")).Text,
                Lastname = ((TextBox)FindName("lastnameTextBox")).Text,
                Patronymic = ((TextBox)FindName("patronymicTextBox")).Text,
                Username = ((TextBox)FindName("usernameTextBox")).Text,
                Password = ((TextBox)FindName("passwordTextBox")).Text
            };

            if (user.Firstname.Equals("") ||
                user.Lastname.Equals("") ||
                user.Patronymic.Equals("") ||
                user.Username.Equals("") ||
                user.Password.Equals(""))
            {
                ((Label)FindName("errorLabel")).Content = "Заполните все поля для регистрации.";
                ((Label)FindName("errorLabel")).Visibility = Visibility.Visible;
                return;
            }
            try
            {
                SaveNewUser(user);
                App.ParentWindowRef.ParentFrame.Navigate(new BooksAccount(user.Username, user.Password));
                App.RegistrationRef = null;
            }
            catch (NameAlreadyExistsException exception)
            {
                ((Label)FindName("errorLabel")).Content = "Такой же логин уже существует.";
                ((Label)FindName("errorLabel")).Visibility = Visibility.Visible;
                Console.WriteLine(exception.StackTrace);
                Console.WriteLine(exception.Message);
            }
            catch (Exception exc)
            {
                ((Label)FindName("errorLabel")).Content = "Произошли технические неполадки. Повторите регистрацию позднее.";
                ((Label)FindName("errorLabel")).Visibility = Visibility.Visible;
                Console.WriteLine(exc.StackTrace);
                Console.WriteLine(exc.Message);
            }
        }

        private void SaveNewUser(User user)
        {
            using (EntityContext db = new EntityContext())
            {
                if (UserExists(db, user.Username))
                    throw new NameAlreadyExistsException("This username already exists.");

                db.Users.AddRange(user);
                db.SaveChanges();
            }
        }

        private bool UserExists(EntityContext entityContext, string username)
        {
            int id = 0;
            id = entityContext.Users.
                Where(u => u.Username == username).
                Select(u => u.Id).
                SingleOrDefault();
            if (id != 0)
            {
                return true;
            }
            else return false;
        }
    }
}
