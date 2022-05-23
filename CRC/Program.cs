using System;
using System.Collections; 
using System.Linq; 

class CRC{

    static BitArray generateBinary(int N){
        var sequence = new BitArray(N);  
        for(int i = 0; i < N; i++){
            Random rand = new Random(); 
            var num = (rand.Next() % 2 == 0);
            sequence[i] = num; 
        }
        return sequence;  
    }

    static String ToString(BitArray buffer){
      var characterArray = buffer.Cast<bool>().Select(b => b ? "1":"0").ToList(); 
      return string.Join("", characterArray); 
    }

    static BitArray cyclicRedundancyCheck(BitArray buffer){ 
        var CRC = new BitArray(new []{true, true, false, true, true, true, true});  //CRC Polynomial 
        //var CRC = new BitArray(new []{true, false, true, true});
        for(int bufferPos = 0; bufferPos < (buffer.Length - CRC.Length); bufferPos++){
          if(buffer.Get(bufferPos) == false){
            continue; 
          }
          for(int FramePos = 0; FramePos < CRC.Length; FramePos++){
            var result = buffer.Get(bufferPos + FramePos) ^ CRC.Get(FramePos);    
            buffer.Set(FramePos + bufferPos, result); 
          }


        }
        var degree = CRC.Cast<bool>().Where(b => b == true).Count(); 
        var remainder = buffer.Cast<bool>().TakeLast<bool>(degree); 
        return new BitArray(remainder.ToArray()); 
    }

    static BitArray AddPadding(BitArray buffer, int paddingSize ){
      var padding = Enumerable.Repeat(false, paddingSize);
      var bits = buffer.Cast<bool>().ToList();  
      bits.AddRange(padding); 
      var padded = new BitArray(bits.ToArray()); 

      return padded; 
    }
 

    static void Main(){
      var binary = generateBinary(1600); 
      Console.WriteLine($"Binary Sequence: {ToString(binary)}"); 

      //var test = new BitArray(new []{true, true, false, true, false, false, true, true, true, false, true, true, false, false});
      binary = AddPadding(binary, 6); 
      var crc = cyclicRedundancyCheck(binary); 

       Console.WriteLine($"The CRC bits are: {ToString(crc)}");   
    }
}
