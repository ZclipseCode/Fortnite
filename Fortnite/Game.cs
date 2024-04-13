﻿using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

public class Game : GameWindow
{
    float[] vertices =
    {
        0f, 0.5f, 0f,       // top vertex
        -0.5f, -0.5f, 0f,   // bottom left vertex
        0.5f, -0.5f, 0f     // bottom right vertex
    };

    // render pipeline variables
    int vao;
    int shaderProgram;

    // constants
    int width;
    int height;

    public Game(int width, int height) : base(GameWindowSettings.Default, NativeWindowSettings.Default)
    {
        this.width = width;
        this.height = height;

        // center the window on monitor
        this.CenterWindow(new Vector2i(width, height));
    }

    protected override void OnResize(ResizeEventArgs e)
    {
        base.OnResize(e);
        GL.Viewport(0, 0, e.Width, e.Height);
        this.width = e.Width;
        this.height = e.Height;
    }

    protected override void OnLoad()
    {
        base.OnLoad();

        vao = GL.GenVertexArray();

        int vbo = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

        // bind the vao
        GL.BindVertexArray(vao);
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexArrayAttrib(vao, 0);

        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        GL.BindVertexArray(0);

        // create the shader program
        shaderProgram = GL.CreateProgram();

        int vertexShader = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(vertexShader, LoadShaderSource("Default.vert"));
        GL.CompileShader(vertexShader);

        int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(fragmentShader, LoadShaderSource("Default.frag"));
        GL.CompileShader(fragmentShader);

        GL.AttachShader(shaderProgram, vertexShader);
        GL.AttachShader(shaderProgram, fragmentShader);

        GL.LinkProgram(shaderProgram);

        // delete the shaders
        GL.DeleteShader(vertexShader);
        GL.DeleteShader(fragmentShader);
    }

    protected override void OnUnload()
    {
        base.OnUnload();

        GL.DeleteVertexArray(vao);
        GL.DeleteProgram(shaderProgram);
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        GL.ClearColor(0.6f, 0.3f, 1f, 1f);
        GL.Clear(ClearBufferMask.ColorBufferBit);

        // draw triangle
        GL.UseProgram(shaderProgram);
        GL.BindVertexArray(vao);
        GL.DrawArrays(PrimitiveType.TriangleFan, 0, 3);

        Context.SwapBuffers();

        base.OnRenderFrame(args);
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        base.OnUpdateFrame(args);
    }

    // loads a text file and return its contents as a string
    public static string LoadShaderSource(string filePath)
    {
        string shaderSource = "";

        try
        {
            using (StreamReader reader = new StreamReader("../../../Shaders/" + filePath))
            {
                shaderSource = reader.ReadToEnd();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Failed to load shader source file: " + e.Message);
        }

        return shaderSource;
    }
}