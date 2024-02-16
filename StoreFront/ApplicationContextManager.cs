using Labb2;

namespace StoreFront;

public static class ApplicationContextManager
{
    public static Labb1TobiasLindbergContext? Context { get; set; }

    public static void Initialize(Labb1TobiasLindbergContext context)
    {
        Context = context;
    }
}