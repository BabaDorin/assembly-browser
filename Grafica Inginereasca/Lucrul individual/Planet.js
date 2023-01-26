class Planet {
  constructor(r, d, o, img) {
    this.v = p5.Vector.random3D();

    this.radius = r;
    this.distance = d;
    this.v.mult(this.distance);
    this.angle = random(TWO_PI);
    this.orbitspeed = o;

    this.planets = null;
    this.texture = img;
  }

  orbit() {
    this.angle = this.angle + this.orbitspeed;
    if (this.planets != null) {
      for (let i = 0; i < this.planets.length; i++) {
        this.planets[i].orbit();
      }
    }
  }

  spawnEarth(){
    this.planets = [];
    let rEarth = this.radius / 2;
    let dEarth = (this.radius + rEarth) * 3;
    let oEarth = 0.01;
    this.planets[0] = new Planet(rEarth, dEarth, oEarth, textures[0]);
    
    let earth = this.planets[0];
    
    let rMoon = this.radius / 5;
    let dMoon = (this.radius + rMoon) * 0.8;
    let oMoon = 0.1;
  
    earth.planets = [];
    earth.planets[0] = new Planet(rMoon, dMoon, oMoon, textures[1])
  }


  show() {
    push();
    noStroke();
    let v2 = createVector(1, 0, 1);
    let p = this.v.cross(v2);

    if (p.x != 0 || p.y != 0 || p.z != 0) {
      rotate(this.angle, p);
    }
    stroke(255);

    translate(this.v.x, this.v.y, this.v.z);
    noStroke();
    fill(255);
    texture(this.texture);
    sphere(this.radius);
    if (this.planets != null) {
      for (let i = 0; i < this.planets.length; i++) {
        this.planets[i].show();
      }
    }
    pop();
  }
}