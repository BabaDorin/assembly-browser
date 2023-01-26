function setup() {
  createCanvas(400, 400, WEBGL);
  truck = loadModel('truck.obj');
  
  road = loadModel('road.obj');
  roadTexture = loadImage('road-texture.jpg');
  
  tree1 = loadModel('Tree.obj');
  
  car = loadModel('Humvee.obj');
}

function draw() {
  background(0);
  ambientLight(100);
  normalMaterial();
  camera(400, -200, 100);
  scale(1, -1, 1);
  scale(40)
  model(truck);
  
  scale(3);
  translate(0, -0.5, 0)
  model(road);
  
  translate(-2, 0, -3);
  scale(0.6)
  model(tree1);
  
  translate(0, 0, 8);
  model(tree1);
  
  translate(0, 0, 0);
  model(tree1);
  
  translate(0, 0, -3);
  model(tree1);
  
  translate(0, 0, -3);
  model(tree1);
  
  translate(5, 0.44, 2);
  scale(0.007)
  rotateY(PI);
  model(car);
}