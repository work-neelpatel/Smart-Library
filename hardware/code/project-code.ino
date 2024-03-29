#include <SPI.h>
#include <MFRC522.h>
#define SS_PIN D4
#define RST_PIN D2
#define PIN1 D0

MFRC522 mfrc522(SS_PIN, RST_PIN); // Instance of the class
void setup() {
   pinMode(PIN1,OUTPUT);
   Serial.begin(9600);
   SPI.begin();       // Init SPI bus
   mfrc522.PCD_Init(); // Init MFRC522
   Serial.println("RFID reading UID");
}
void loop() {
if ( mfrc522.PICC_IsNewCardPresent())
    {
        if ( mfrc522.PICC_ReadCardSerial())
        {
           //Serial.print("Tag UID:");
           for (byte i = 0; i < mfrc522.uid.size; i++) {
                  Serial.print(mfrc522.uid.uidByte[i] < 0x10 ? "0" : "");
                  Serial.print(mfrc522.uid.uidByte[i], HEX);
            }
            digitalWrite(PIN1,LOW);
            delay(500);
            digitalWrite(PIN1,HIGH);
            Serial.println();
            mfrc522.PICC_HaltA();
        }
  }
}
