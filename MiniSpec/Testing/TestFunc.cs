namespace MiniSpec.Testing {
  public delegate T TestFunc<T>();
  public delegate T TestFunc<K, T>(K argument);
}