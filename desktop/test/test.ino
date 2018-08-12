int PlanePin = A5;
int bulbPin = A4;
int pin = 8;
void setup() {
    // put your setup code here, to run once:
    Serial.begin(9600);
    pinMode(PlanePin,INPUT);
    pinMode(bulbPin,INPUT);
}

void loop() {
    // put your main code here, to run repeatedly:
    int val = analogRead(PlanePin);
    if(val > 800 || val < 100){
         Serial.println("read from 5 ");
         Serial.println(val);
    }
    val = 300;
    delay(5);
    val = analogRead(bulbPin);
    if(val > 800 || val < 100){
         Serial.println("read from 4 ");
         Serial.println(val);
    }
    val = 300;
    delay(5);

     val = analogRead(3);
    if(val > 800 || val < 100){
         Serial.println("read from 3 ");
         Serial.println(val);
    }
    val = 300;
    delay(5);

     val = analogRead(2);
    if(val > 800 || val < 100){
         Serial.println("read from 2 ");
         Serial.println(val);
    }
    val = 300;
    delay(5);


     val = analogRead(1);
    if(val > 800 || val < 100){
         Serial.println("read from 1 ");
         Serial.println(val);
    }
    val = 300;
    delay(5);

     val = analogRead(0);
    if(val > 800 || val < 100){
         Serial.println("read from 0 ");
         Serial.println(val);
    }
    val = 300;
    delay(5);
}
