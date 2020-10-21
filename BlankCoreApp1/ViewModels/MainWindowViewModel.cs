﻿using PasswordCreator;
using Prism.Commands;
using Prism.Mvvm;
using System;

namespace BlankCoreApp1.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region メンバ変数
        private ILetterFactory factory;
        #endregion

        #region プロパティ
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private bool isNonMark = false;
        public bool IsNonMark
        {
            get { return isNonMark; }
            set
            {
                SetProperty(ref isNonMark, value);
                //if (isNonMark)
                //{
                //    factory = new NonMarkLetterFactory();
                //}
                //else
                //{
                //    factory = new AllLetterFactory();
                //}
                factory = isNonMark
                    ? (ILetterFactory)new NonMarkLetterFactory()
                    : new AllLetterFactory();
            }
        }
        private int numOfLetters;
        public int NumOfLetters
        {
            get { return numOfLetters; }
            set { SetProperty(ref numOfLetters, value); }
        }

        private string createdPassword;
        public string CreatedPassword
        {
            get { return createdPassword; }
            set { SetProperty(ref createdPassword, value); }
        }

        public DelegateCommand Generate { get; private set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainWindowViewModel()
        {
            NumOfLetters = 20;
            IsNonMark = false;
            CreatedPassword = "This is Prism WPF App.";
            Generate = new DelegateCommand(
                () => { CreatePasswordExecute(); },
                () => CanMakePasswordExecute()
            );
        }
        #endregion

        #region メソッド
        private void CreatePasswordExecute()
        {
            Random random = new Random();
            var generator = new PasswordGenerator(random);
            CreatedPassword = generator.MakePassword(NumOfLetters, factory);
        }

        public bool CanMakePasswordExecute()
        {
            return numOfLetters > 10;
        }
        #endregion
    }
}
