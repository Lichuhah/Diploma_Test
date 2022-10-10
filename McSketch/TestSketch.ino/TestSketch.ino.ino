#include <ESP8266WiFi.h>
#include <ESP8266WebServer.h>
#include <ESP8266SSDP.h>
#include <FS.h>

IPAddress apIP(192, 168, 4, 1);

// Web интерфейс для устройства
ESP8266WebServer HTTP(80);

File fsUploadFile;

// Определяем переменные wifi
String _ssid     = "TP-LINK_F7FE"; // Для хранения SSID
String _password = "393-63-461"; // Для хранения пароля сети
String _ssidAP = "WiFi";   // SSID AP точки доступа
String _passwordAP = ""; // пароль точки доступа
String SSDP_Name="SSDP-Shanti";

void setup() {
  Serial.begin(115200);
  Serial.println("");
  Serial.println("Start 0-FS");
  FS_init();
  Serial.println("Start 1-WIFI");
  WIFIinit();
  Serial.println("Start 2-SSDP");
  SSDP_init();
  Serial.println("Start 3-WebServer");
  HTTP_init();
}

void loop() {
  HTTP.handleClient();
  delay(1);
}



