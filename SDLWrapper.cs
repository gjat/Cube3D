using SDL2;
using Shapes3D;

public class SDLWrapper
{
    public IntPtr Window { get; private set; }
    public IntPtr Renderer { get; private set;}

    public int ViewWidth { get; private set; }
    public int ViewHeight { get; private set; }
    public int FOV { get; private set; }

    public bool SetupSDL(string windowTitle, int viewWidth, int viewHeight, int fov)
    {
        if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO) < 0)
        {
            Console.WriteLine($"Can't initialise SDL. {SDL.SDL_GetError()}");
            return false;
        }

        Window = SDL.SDL_CreateWindow(windowTitle, SDL.SDL_WINDOWPOS_UNDEFINED, SDL.SDL_WINDOWPOS_UNDEFINED, viewWidth, viewHeight, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);

        if (Window == IntPtr.Zero)
        {
            Console.WriteLine("Can't create SDL Window. {SDL.SDL_GetError()}");
            return false;
        }

        ViewWidth = viewWidth;
        ViewHeight = viewHeight;
        FOV = fov;

        // Creates a new SDL hardware renderer using the default graphics device with VSYNC enabled.
        Renderer = SDL.SDL_CreateRenderer(Window, 
                                                -1, 
                                                SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | 
                                                SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);

        if (Renderer == IntPtr.Zero)
        {
            Console.WriteLine($"There was an issue creating the renderer. {SDL.SDL_GetError()}");
            return false;
        }

        // Initilizes SDL_image for use with png files.
        if (SDL_image.IMG_Init(SDL_image.IMG_InitFlags.IMG_INIT_PNG) == 0)
        {
            Console.WriteLine($"There was an issue initilizing SDL2_Image {SDL_image.IMG_GetError()}");
            return false;
        }

        return true;
    }

    public void CleanupSDL()
    {
        // Clean up the resources that were created.
        SDL.SDL_DestroyRenderer(Renderer);
        SDL.SDL_DestroyWindow(Window);
        SDL.SDL_Quit();
    }

    public void RenderScene(Scene3D scene)
    {
        // Sets the color that the screen will be cleared with.
        if (SDL.SDL_SetRenderDrawColor(Renderer, 0, 0, 127, 255) < 0)
        {
            Console.WriteLine($"There was an issue with setting the render draw color. {SDL.SDL_GetError()}");
        }

        // Clears the current render surface.
        if (SDL.SDL_RenderClear(Renderer) < 0)
        {
            Console.WriteLine($"There was an issue with clearing the render surface. {SDL.SDL_GetError()}");
        }

        // Set the color to red before drawing our shape
        SDL.SDL_SetRenderDrawColor(Renderer, 127, 255, 0, 255);

        foreach(var line in scene.lines)
        {
            int x1, y1;
            int x2, y2;

            x1 = ToView(FromScene(line.A.X, ViewWidth) * FOV / ( line.A.Z + FOV ), ViewWidth);
            y1 = ToView(FromScene(line.A.Y, ViewHeight) * FOV / ( line.A.Z + FOV ), ViewHeight);

            x2 = ToView(FromScene(line.B.X, ViewWidth) * FOV / ( line.B.Z + FOV ), ViewWidth);
            y2 = ToView(FromScene(line.B.Y,ViewHeight) * FOV / ( line.B.Z + FOV ), ViewHeight);

            // Draw a line from top left to bottom right
            SDL.SDL_RenderDrawLine(Renderer, x1, y1, x2, y2);
        }

        // Switches out the currently presented render surface with the one we just did work on.
        SDL.SDL_RenderPresent(Renderer);
    }

    static int FromScene(int value, int sceneSize)
    {
        return value - (sceneSize / 2);
    }

    static int ToView(int value, int viewSize)
    {
        return value + (viewSize / 2);
    }
}