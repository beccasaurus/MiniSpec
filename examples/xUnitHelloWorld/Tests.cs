#pragma warning disable 8321

using System;

MiniSpec.Spec.Describe("Dog", dog => {
  dog.Can("Bark");
});

MiniSpec.Tests.Run(Console.Out, Console.Error, args);

class DogTests {
    void ItCanBark() {}
    void ItCanSit() {
        throw new System.Exception("Kaboom!");
    }
}