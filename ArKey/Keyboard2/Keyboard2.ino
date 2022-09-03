#include <Debounce.h>

// constants won't change. They're used here to set pin numbers:
const int btn1 = 2; // the number of the pushbutton pin
const int btn2 = 3;
const int btn3 = 4;
const int btn4 = 5;
const int btn5 = 6;
const int btn6 = 7;
const int btn7 = 8;
const int btn8 = 9;
const int btn9 = 10;
const int btn10 = 11;
const int btn11 = 12;
const int btn12 = 13;

Debounce Btn1(btn1);
Debounce Btn2(btn2);
Debounce Btn3(btn3);
Debounce Btn4(btn4);
Debounce Btn5(btn5);
Debounce Btn6(btn6);
Debounce Btn7(btn7);
Debounce Btn8(btn8);
Debounce Btn9(btn9);
Debounce Btn10(btn10);
Debounce Btn11(btn11);
Debounce Btn12(btn12);

void setup() {
  // initialize serial communication at 9600 bits per second:
  Serial.begin(9600);
  // initialize the pushbutton pin as an pull-up input
  // the pull-up input pin will be HIGH when the switch is open and LOW when the switch is closed.  
  pinMode(btn1, INPUT_PULLUP); 
  pinMode(btn2, INPUT_PULLUP);
  pinMode(btn3,INPUT_PULLUP);
  pinMode(btn4, INPUT_PULLUP); 
  pinMode(btn5, INPUT_PULLUP);
  pinMode(btn6,INPUT_PULLUP);
  pinMode(btn7,INPUT_PULLUP);
  pinMode(btn8,INPUT_PULLUP);
  pinMode(btn9,INPUT_PULLUP);
  pinMode(btn10,INPUT_PULLUP);
  pinMode(btn11,INPUT_PULLUP);
  pinMode(btn12,INPUT_PULLUP);

  Serial.println("Reading Button States:");
}

bool btn1c;
bool btn2c;
bool btn3c;
bool btn4c;
bool btn5c;
bool btn6c;
bool btn7c;
bool btn8c;
bool btn9c;
bool btn10c;
bool btn11c;
bool btn12c;

void loop() {
  //BTN1 Example
  if(Btn1.read())
  {
      Serial.println("btn1_down");
      btn1c = true;
  }
  else
  {
    if(btn1c)
    {
      Serial.println("btn1_up");
      btn1c = false;
      delay(100);
    }
  }
  //BTN2
  if(Btn2.read())
  {
      Serial.println("btn2_down");
      btn2c = true;
  }
  else
  {
    if(btn2c)
    {
      Serial.println("btn2_up");
      btn2c = false;
      delay(100);
    }
  }
  //BTN3
  if(Btn3.read())
  {
      Serial.println("btn3_down");
      btn3c = true;
  }
  else
  {
    if(btn3c)
    {
      Serial.println("btn3_up");
      btn3c = false;
      delay(100);
    }
  }
  //BTN4
  if(Btn4.read())
  {
      Serial.println("btn4_down");
      btn4c = true;
  }
  else
  {
    if(btn4c)
    {
      Serial.println("btn4_up");
      btn4c = false;
      delay(100);
    }
  }
  //BTN5
  if(Btn5.read())
  {
      Serial.println("btn5_down");
      btn5c = true;
  }
  else
  {
    if(btn5c)
    {
      Serial.println("btn5_up");
      btn5c = false;
      delay(100);
    }
  }
  //BTN6
  if(Btn6.read())
  {
      Serial.println("btn6_down");
      btn6c = true;
  }
  else
  {
    if(btn6c)
    {
      Serial.println("btn6_up");
      btn6c = false;
      delay(100);
    }
  }

  //BTN7
  if(Btn7.read())
  {
      Serial.println("btn7_down");
      btn7c = true;
  }
  else
  {
    if(btn7c)
    {
      Serial.println("btn7_up");
      btn7c = false;
      delay(100);
    }
  }

  //BTN8
  if(Btn8.read())
  {
      Serial.println("btn8_down");
      btn8c = true;
  }
  else
  {
    if(btn8c)
    {
      Serial.println("btn8_up");
      btn8c = false;
      delay(100);
    }
  }  

  //BTN9
  if(Btn9.read())
  {
      Serial.println("btn9_down");
      btn9c = true;
  }
  else
  {
    if(btn9c)
    {
      Serial.println("btn9_up");
      btn9c = false;
      delay(100);
    }
  }

  //BTN10
  if(Btn10.read())
  {
      Serial.println("btn10_down");
      btn10c = true;
  }
  else
  {
    if(btn10c)
    {
      Serial.println("btn10_up");
      btn10c = false;
      delay(100);
    }
  }

  //BTN11
  if(Btn11.read())
  {
      Serial.println("btn11_down");
      btn11c = true;
  }
  else
  {
    if(btn11c)
    {
      Serial.println("btn11_up");
      btn11c = false;
      delay(100);
    }
  }

  //BTN12
  if(Btn12.read())
  {
      Serial.println("btn12_down");
      btn12c = true;
  }
  else
  {
    if(btn12c)
    {
      Serial.println("btn12_up");
      btn12c = false;
      delay(100);
    }
  }
}
