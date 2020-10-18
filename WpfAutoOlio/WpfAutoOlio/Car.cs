using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace WpfAutoOlio
{
    public class Car
    {
        public string Color { get; set; }
        public int MaxSpeed {
            get
            {
                return maxSpeed;
                //maksiminopeus

            }
            set
            {
                if ((value >= 0) && (value <= 400))
                {
                    maxSpeed = value;
                } else
                {
                    maxSpeed = 0;
                    throw new ArgumentOutOfRangeException();
                }
            }
        }
        public Boolean Running { get; set; }
        public string Model { get; set; }
        public int CurrentSpeed { get; set; }
        public int HorsePower { get; set; }
        public string TransMission { get; set; }
        public int GearCount {
            get
            {
                return gearCount;
            }
            set
            {
                if (Regex.IsMatch(value.ToString(), "[4-9]"))
                {
                    gearCount = value;
                }
                else
                {
                    if (value == 0)
                    {
                        gearCount = 0;
                    }
                    else
                    {
                    gearCount = 0;
                    throw new ArgumentOutOfRangeException();
                    }
                    
                }
            }
        }
        private int gearCount;
        private int maxSpeed;

        public void StartEngine()
        {
            Running = true;
        }

        public void StopEngine()
        {
            Running = false;
        }

        public void Accelerate()
        {
            CurrentSpeed++;
        }

        public void Brake()
        {
            CurrentSpeed--;
        }

        public void SetMaxSpeed(int MaxSpeedi)
        {
            if ((MaxSpeedi >= 0) && (MaxSpeedi <= 400))
            {
                maxSpeed = MaxSpeedi;
            } else
            {
                maxSpeed = 0;
                throw new ArgumentOutOfRangeException();
            }
        }

        public int GetMaxSpeed()
        {
            return maxSpeed;
        }
    }
}
