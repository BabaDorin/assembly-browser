let angleX = 0;
let angleY = 0;
let angleZ = 0;
let yPos = 0;
let time = 0;
let truck;

function setup() {
  createCanvas(800, 600, WEBGL);
  truck = loadModel('truck.obj');
}

function draw() {
  background(0);
  ambientLight(100);
  normalMaterial();
  camera(400, -200, 100);
  scale(1, -1, 1);
  scale(40)
  translate(0, yPos, 0);
  rotateX(angleX);
  rotateY(angleY);
  rotateZ(angleZ);
  model(truck);
  time += 0.01;
  if (time < 2) {
    angleX += 0.01;
  } else if (time < 4) {
    angleY += 0.01;
  } else if (time < 6) {
    angleZ += 0.01;
  }
}