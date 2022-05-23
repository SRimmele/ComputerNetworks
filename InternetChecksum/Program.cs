using System;

class InternetChecksum{

    static UInt16[] generateBinary(int N){
        var words = new UInt16[N];  
        for(int i = 0; i < N; i++){
            Random rand = new Random(); 
            var num = (UInt16)(rand.Next() % UInt16.MaxValue);
            words[i] = num; 
        }
        return words;  
    }

    static UInt16 checksum(UInt16[] buffer){
        UInt64 sum = 0; 
        foreach(var word in buffer){
          sum += word; 
          if ((sum & 0xFFFF0000) != 0){
            sum &= 0xFFFF; 
            sum++; 
          }
        }
      return (UInt16)(~(sum & 0xFFFF)); 
    }

    static void modified(UInt16[] buffer){
      buffer[4] ^= 0b100;
      buffer[7] ^= 0b100; 
    }


  
    static void Main(){
      var binary = generateBinary(100);
      Console.Write("Original Binary Sequence: "); 
      foreach(var word in binary){
        Console.Write(Convert.ToString(word, 2).PadLeft(16, '0')); 
      } 
      Console.WriteLine(); 

      var chksum= checksum(binary); 
      Console.Write("Checksum for Original Binary Sequence: "); 
      Console.WriteLine(Convert.ToString(chksum, 2).PadLeft(16, '0')); 
      Console.WriteLine(); 

      modified(binary); 
      Console.Write("Modified Binary Sequence: "); 
      foreach(var word in binary){
        Console.Write(Convert.ToString(word, 2).PadLeft(16, '0')); 
      } 
      Console.WriteLine(); 

      var modifyChecksum = checksum(binary); 
      Console.Write("Checksum of Modified Sequences: "); 
      Console.WriteLine(Convert.ToString(modifyChecksum, 2).PadLeft(16, '0')); 
       
    }
}
