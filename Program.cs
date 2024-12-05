// SDL knowledge came from: https://jsayers.dev/c-sharp-sdl-tutorial-part-1-setup/
// How to rotate 3D shapes, came from: https://www.khanacademy.org/computing/computer-programming/programming-games-visualizations/programming-3d-shapes/a/rotating-3d-shapes

using SDL2;
using Shapes3D;

Scene3D scene = new Scene3D();

const int ViewWidth = 640;
const int ViewHeight = 480;
const int FOV = 300;
const int CubeSize = 100;

void CreateScene()
{
    var cubeSize = CubeSize;
    scene.Add(new Line3D(-cubeSize, -cubeSize, -cubeSize, cubeSize, -cubeSize, -cubeSize));
    scene.Add(new Line3D(cubeSize, -cubeSize, -cubeSize, cubeSize, cubeSize, -cubeSize));
    scene.Add(new Line3D(cubeSize, cubeSize, -cubeSize, -cubeSize, cubeSize, -cubeSize));
    scene.Add(new Line3D(-cubeSize, cubeSize, -cubeSize, -cubeSize, -cubeSize, -cubeSize));

    scene.Add(new Line3D(-cubeSize, -cubeSize, cubeSize, cubeSize, -cubeSize, cubeSize));
    scene.Add(new Line3D(cubeSize, -cubeSize, cubeSize, cubeSize, cubeSize, cubeSize));
    scene.Add(new Line3D(cubeSize, cubeSize, cubeSize, -cubeSize, cubeSize, cubeSize));
    scene.Add(new Line3D(-cubeSize, cubeSize, cubeSize, -cubeSize, -cubeSize, cubeSize));

    scene.Add(new Line3D(-cubeSize, -cubeSize, -cubeSize, -cubeSize, -cubeSize, cubeSize));
    scene.Add(new Line3D(cubeSize, -cubeSize, -cubeSize, cubeSize, -cubeSize, cubeSize));
    scene.Add(new Line3D(cubeSize, cubeSize, -cubeSize, cubeSize, cubeSize, cubeSize));
    scene.Add(new Line3D(-cubeSize, cubeSize, -cubeSize, -cubeSize, cubeSize, cubeSize));
}

static double DegreesToRadians(int degrees)
{
    return degrees * Math.PI / 180;
}

// ---------------------------------------------------------------
var sdlWrapper = new SDLWrapper();

if(sdlWrapper.SetupSDL("Cube 3D", ViewWidth, ViewHeight, FOV) != true)
    return;

CreateScene();

var running = true;
var rotationX = 0;
var rotationY = 0;
var bounceX = 100;
var bounceY = 240;
var directionX = 5;
var directionY = 5;

while (running)
{
    // Check to see if there are any events and continue to do so until the queue is empty.
    while (SDL.SDL_PollEvent(out SDL.SDL_Event e) == 1)
    {
        switch (e.type)
        {
            case SDL.SDL_EventType.SDL_QUIT:
                running = false;
                break;
        }
    }

    var screenScene = scene.RotateX(DegreesToRadians(rotationX))
        .RotateY(DegreesToRadians(rotationY))
        .TranslateScene(bounceX, bounceY, CubeSize*2);

    sdlWrapper.RenderScene(screenScene);

    rotationX += 1;  if (rotationX >= 360) rotationX = 0;
    rotationY += 1;  if (rotationY >= 360) rotationY = 0;

    bounceX += directionX;
    if(bounceX < CubeSize)
    {
        directionX = -directionX;
        bounceX = CubeSize;
    }

    if(bounceX > ViewWidth - CubeSize)
    {
        directionX = -directionX;
        bounceX = ViewWidth - CubeSize;
    }

    bounceY += directionY;
    if(bounceY < CubeSize)
    {
        directionY = -directionY;
        bounceY = CubeSize;
    }

    if(bounceY > ViewHeight - CubeSize)
    {
        directionY = -directionY;
        bounceY = ViewHeight - CubeSize;
    }
}

sdlWrapper.CleanupSDL();