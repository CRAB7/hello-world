public class SelectionSorter{
  public static void sort(int[] a){
    for (int i = 0; i < a.length - 1; i++){
      int minPos = minimumPosition(a,i);
      swap(a, minPos, i);
    }
  }
  
  private static int minimumPosition(int[] a, int from){
    int minPos = from;
    for (int i = from + 1; i < a.length; i++){
      if (a[i] < a[minPos]) minPos = i;
    }
    return minPos;
  }
  
  private static int[] swap(int[] a, int e1, int e2){
    int temp = a[e1];
    a[e1] = a[e2];
    a[e2] = temp;
  }
}
