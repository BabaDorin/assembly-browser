function setup() {
  createCanvas(400, 400, WEBGL);
  
  rock = loadModel('Rock1.obj');
  lighthouse = loadModel('lighthouse.obj');
  tree = loadModel('Tree.obj');
}

function draw() {
  background(0);
  
  ambientLight(100);
  normalMaterial();
  camera(350, -350, 200);
  scale(1, -1, 1);
  
  scale(3);
  model(lighthouse);
  
  scale(40);
  translate(0, -1.5, -0.5);
  model(rock);
  
  translate(0, 2, 0);
  scale(0.1)
  model(tree);
  
  translate(14, 2, 10);
  model(tree);
}