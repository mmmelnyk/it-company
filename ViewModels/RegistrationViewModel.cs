﻿using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using it_company.Models;
using it_company.Repository;
using it_company.Views;

namespace it_company.ViewModels
{
    public class RegistrationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public event EventHandler Closing;

        private bool Validate = false;

        private RelayCommand _register;
        private RelayCommand _login;
        private RelayCommand _exit;

        private string _fName;
        private string _lName;
        private string _email;
        private string _password;


        public string FirstName
        {
            get => _fName;
            set
            {
                _fName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string LastName
        {
            get => _lName;
            set
            {
                _lName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public RelayCommand Register
        {
            get
            {
                return _register ??
                    (_register = new RelayCommand(o =>
                    {
                        if (Validate)
                        {
                            MessageBox.Show("Please, check if all fields were filled in correct way! ", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }


                        using (DataContext dataContext = new DataContext())
                        {
                            UserRepository userRepository = new UserRepository(dataContext);
                            DepartmentRepository depRep = new DepartmentRepository(dataContext);

                            var user = userRepository.GetAll(i => i.Email == Email).FirstOrDefault();

                            if (user != null)
                            {
                                MessageBox.Show("User with this Email already exists");
                                return;
                            }

                            if (depRep.GetAll(i => i.Title == "AllStaff") == null)
                            {
                                depRep.Add(new Department()
                                {
                                    Title = "AllStaff"
                                });
                            }

                            user = new User()
                            {
                                Email =_email,
                                FName = _fName,
                                LName = _lName,
                                PasswordHash = _password.GetHashCode(),
                                DepartmentId = depRep.GetAll(i => i.Title == "AllStaff").Find(i => i.Title == "AllStaff").DepartmentId
                            };

                            userRepository.Add(user);
                            dataContext.SaveChanges();

                            MessageBox.Show($"Nice to see you here, {user.FName} {user.LName} !", "Welcome!", MessageBoxButton.OK, MessageBoxImage.Information);

                            Login lgn = new Login(_email);
                            lgn.Show();
                        }

                        Closing?.Invoke(this, EventArgs.Empty);
                    }));
            }
        }

        public RelayCommand Login
        {
            get
            {
                return _login ??
                    (_login = new RelayCommand(o =>
                    {
                        Login login = new Login();
                        
                        login.Show();
                        Closing?.Invoke(this, EventArgs.Empty);
                    }));
            }
        }

        public RelayCommand Exit
        {
            get
            {
                return _exit ??
                    (_exit = new RelayCommand(o =>
                    {
                        
                        Environment.Exit(0);
                    }));
            }
        }
    }
}
