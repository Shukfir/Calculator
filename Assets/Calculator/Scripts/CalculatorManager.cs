﻿using System;
using UnityEngine;
using UnityEngine.UI;

namespace Calculator
{
    //Перечисление, в котором описанно, что мы отображаем на экране
    public enum InputStates
    {
        FirstParam,
        SecondParam,
        Result
    }

    //Перечисление, в котором описанно, знаки нашего калькулятора
    public enum Sign
    {
        Equal,
        Plus
    }
    //На скрипт, который управляет нашим калькулятором
    public class CalculatorManager : MonoBehaviour
    {
        //Поле, где мы храним наши кнопки с цифрами
        public Button[] numberButtons;
        //Поле, где мы храним кнопку плюс
        public Button plusButton;
        //Поле, где мы храним кнопку равно
        public Button equalButton;
        //Поле, на котором мы отображаем наши значения на экране
        public Text output;

        //Поле, где мы храним значение нашего первого введенного числа
        private string _firstParam;
        //Поле, где мы храним значение нашего второго введенного числа
        private string _secondParam;
        
        //Поле, в котором мы храним что написанно на нашем экране 
        private InputStates _inputStates;
        //Поле, в котором мы храним нажатый последний знак
        private Sign _sign;
    
        //Метод (Функция), которая будет вызываться Unity в момент появления нашего каркаса с калькулятором на сцене
        void Start()
        {
            //Мы проходимся по массиву с цировыми кнопками, чтобы придать действие на нажатие нашей кнопки с цифрой
            for (var i = 0; i < numberButtons.Length; i++)
            {
                //Замыкание - мы копируем порядковый номер нашей кнопки, чтобы использовать его для передачи в метод обработки нажатий цифровых кнопок
                var i1 = i;
                //Подписываемся на событие нажатия кнопки с цифрой
                numberButtons[i].onClick.AddListener(
                    () =>
                    {
                        //Вызываем метод и передаем копию порядкого номера (по факту, значением кнопки)
                        OnNumberButtonClick(i1);
                    });
            }
            //Подписываемся на событие нажатия кнопки с плюсом
            plusButton.onClick.AddListener(OnPlusButtonClick);
            //Подписываемся на событие нажатия кнопки с равно
            equalButton.onClick.AddListener(OnEqualButtonClick);
        }
        
        //Метод, который выводит наш параметр на экран
        private void PrintNumberOnDisplay(string number)
        {
            output.text = number;
        }
        
        //Метод, который будет вызваться по нажатию кнопки с цифрой
        private void OnNumberButtonClick(int number)
        {
            //Используем конструкцию switch по состоянию на дисплеи, чтобы обработать нажатие кнопки
            switch (_inputStates)
            {
                // Случай, когда на экране первое число
                case InputStates.FirstParam:
                    //Добавляем введенную цифру к текущему первому числу на экране
                    //Так как это строка, то результат будет "10" + 1 = "101", не путать с  10 + 1 = 11
                    _firstParam += number;
                    //Обновляем наш экран выводя первое число
                    PrintNumberOnDisplay(_firstParam);
                    //Синтаксис, необходимы для того, чтобы сказать switch, что мы обработали наше состояние и больше не нужно его обрабатыать
                    break;
                // Случай, когда на экране второе число
                case InputStates.SecondParam:
                    //Добавляем введенную цифру к текущему первому числу на экране
                    _secondParam += number;
                    //Обновляем наш экран выводя второе число
                    PrintNumberOnDisplay(_secondParam);
                    //Синтаксис, необходимы для того, чтобы сказать switch, что мы обработали наше состояние и больше не нужно его обрабатыать
                    break;
                // Случай, когда на экране результат
                case InputStates.Result:
                    //Так как у нас результат на экране, то обычно калькулятор при нажатии кнопки начинает заново вводить новое число
                    _inputStates = InputStates.FirstParam;
                    //Добавляем введенную цифру к текущему первому числу на экране
                    _firstParam += number;
                    //Обновляем наш экран выводя результат
                    PrintNumberOnDisplay(_firstParam);
                    //Синтаксис, необходимы для того, чтобы сказать switch, что мы обработали наше состояние и больше не нужно его обрабатыать
                    break;
            }
        }

        //Метод, который будет вызваться по нажатию кнопки плюс
        private void OnPlusButtonClick()
        {
            //Меняем последний нажатый знак на плюс
            _sign = Sign.Plus;
            //Используем конструкцию switch по состоянию на дисплеи, чтобы обработать нажатие кнопки
            switch (_inputStates)
            {
                //Если у нас введено первое чилсо
                case InputStates.FirstParam:
                    //Знак плюс просто меняет состояние, что он ждет следующее число
                    _inputStates = InputStates.SecondParam;
                    //Синтаксис, необходимы для того, чтобы сказать switch, что мы обработали наше состояние и больше не нужно его обрабатыать
                    break;
                //Если у нас введено второе число
                case InputStates.SecondParam:
                    //При нажатии кнопки со знаком, обычно колькулятор думает, что это не второе число и первое,
                    //по этому мы просто записываем в первое число - втроое. 
                    _firstParam = _secondParam;
                    //Синтаксис, необходимы для того, чтобы сказать switch, что мы обработали наше состояние и больше не нужно его обрабатыать
                    break;
                //Если у нас отображается результат
                case InputStates.Result:
                    //При нажатии кнопки с равно , обычно колькулятор думает, что это теперь первое число,
                    //по этому мы просто записываем результат в первое число. 
                    _firstParam = output.text;
                    //И теперь ожидаем ввода пторого числа
                    _inputStates = InputStates.SecondParam;
                    //Синтаксис, необходимы для того, чтобы сказать switch, что мы обработали наше состояние и больше не нужно его обрабатыать
                    break;
            }
        }

        //Метод, который будет вызваться по нажатию кнопки равно
        private void OnEqualButtonClick()
        {
            //Используем конструкцию switch по знаку, чтобы обработать нажатие кнопки 
            switch (_sign)
            {
                //Если был нажат плюс до этого
                case Sign.Plus:
                    //Записываем, что теперь мы нажали равно
                    _sign = Sign.Equal;
                    //Записываем, что теперь у нас будет результат на экране
                    _inputStates = InputStates.Result;
                    //Получем из строчки значение в формате int первого введеного параметра
                    var p1 =  Convert.ToInt32(_firstParam);
                    //Получем из строчки значение в формате int второго введеного параметра
                    var p2 = Convert.ToInt32(_secondParam);
                    //Складываем результаты и приводим (конвертируем) к строке
                    var result = (p1 + p2).ToString();
                    //Обновляем наш экран выводя результат на экран
                    PrintNumberOnDisplay(result);
                    //Синтаксис, необходимы для того, чтобы сказать switch, что мы обработали наше состояние и больше не нужно его обрабатыать
                    break;
                //Если был нажат равно до этого, то мы ничего не делаем, так как мы снова нажали равно
                case Sign.Equal:
                    break;
            }

            //очищаем наши введенные числа
            _firstParam = "0";
            _secondParam = "0";
        }
    }
}
