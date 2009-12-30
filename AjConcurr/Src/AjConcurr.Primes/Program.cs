using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AjConcurr.Primes
{
    class Program
    {
        static void Main(string[] args)
        {
            Channel numbers = new Channel();

            GoRoutines.Go(() => { for (int k = 2; ; k++) numbers.Send(k);  });

            Channel channel = numbers;

            int prime = 0;

            while (prime < 1000)
            {
                prime = (int)channel.Receive();

                Console.WriteLine(prime);

                Channel newchannel = new Channel();

                GoRoutines.Go((input, output, p) =>
                {
                    while (true)
                    {
                        int number = (int)input.Receive();

                        if ((number % p) != 0)
                            output.Send(number);
                    }
                }, channel, newchannel, prime);

                channel = newchannel;
            }
        }
    }
}
