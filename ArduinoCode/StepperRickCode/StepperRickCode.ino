#include <AccelStepper.h>
#define HALFSTEP 4 //Half-step mode (8 step control signal sequence)
#define BAUD_RATE 9600
// Motor pin definitions
#define mtrPin1  4     // IN1 on the ULN2003 driver 1
#define mtrPin2  5     // IN2 on the ULN2003 driver 1
#define mtrPin3  6     // IN3 on the ULN2003 driver 1
#define mtrPin4  7     // IN4 on the ULN2003 driver 1
#define oneTurn  2048
int message = 0;
bool startUp = true;
bool runStepper = false;
AccelStepper stepper1(HALFSTEP, mtrPin1, mtrPin3, mtrPin2, mtrPin4);

void setup() {
  stepper1.setMaxSpeed(1000.0);
  stepper1.setAcceleration(200.0);  //Make the acc quick
  stepper1.setSpeed(350);
  stepper1.moveTo(oneTurn * 3 - 200); //Rotate 1 revolution
  Serial.begin(BAUD_RATE);
}

void loop() {
  if (runStepper)
  {
    stepper1.run();  //Start
    if (stepper1.distanceToGo() == 0 && stepper1.currentPosition() != 0 ) {
      delay(20000);
      stepper1.moveTo(0);
    } else if (stepper1.distanceToGo() == 0  && stepper1.currentPosition() == 0){
      runStepper = false;
      stepper1.moveTo(oneTurn * 3 - 200);
    }
  }

  //trigger sun when you get serial message from windows app 
  if (Serial.available() > 0) { // Check to see if there is a new message

    message = Serial.read(); // Put the serial input into the message

    if (message == 'a') { // If a capitol A is received
      runStepper = true;
    }
  }
  
}
