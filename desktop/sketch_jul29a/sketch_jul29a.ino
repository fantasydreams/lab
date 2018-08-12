int pin = 8;
#define plane 'a'
#define bulb 'b'
#define SP 'c'
int PlaneReadPin = A0;
int BulbReadPin = A1;
int SPReadPin = A2;
void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  pinMode(pin,OUTPUT);
}

void loop() {
    // put your main code here, to run repeatedly:
    int val = analogRead(PlaneReadPin);
    if(val > 800 || val < 100){
         Serial.println(plane);
    }

/*
    val = analogRead(A1);
    if(val > 800 || val < 100){
         Serial.println(bulb);
    }
    
    val = analogRead(A2);
    if(val > 800 || val < 100){
         Serial.println(SP);
    }
  */  
    delay(10);
}
