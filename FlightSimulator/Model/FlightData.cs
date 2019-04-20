using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model
{
    /**
     * Singleton -> the FlightData class is an Indexer that contains all of the current values from the simulator.
     * */
    public class FlightData
    {
        private Dictionary<string, double> data = new Dictionary<string, double>();
        private static FlightData self = null;

        /**
         * private CTOR.
         * */
        private FlightData() { InitializeDataMap(); }

        /**
         * The Instance property getter.
         * */
        public static FlightData Instance
        {
            get
            {
                if (self == null)
                {
                    self = new FlightData();
                }
                return self;
            }
        }

        /**
         * The indexer, returns the value if the key exists, Nan if it doesn't.
         * */
        public double this [string key]
        {
            get
            {
                if (data.ContainsKey(key))
                {
                    return data[key];
                } else
                {
                    return Double.NaN;
                }
            }
        }

        /**
         * Parse the given stream of tokens to double and update the dictionary's values.
         * */
        public void SetDataValues(String[] tokens)
        {
            try
            {
                data["/position/longitude-deg"] = Double.Parse(tokens[0]);
                data["/position/latitude-deg"] = Double.Parse(tokens[1]);
                data["/instrumentation/airspeed-indicator/indicated-speed-kt"] = Double.Parse(tokens[2]);
                data["/instrumentation/altimeter/indicated-altitude-ft"] = Double.Parse(tokens[3]);
                data["/instrumentation/altimeter/pressure-alt-ft"] = Double.Parse(tokens[4]);
                data["/instrumentation/attitude-indicator/indicated-pitch-deg"] = Double.Parse(tokens[5]);
                data["/instrumentation/attitude-indicator/indicated-roll-deg"] = Double.Parse(tokens[6]);
                data["/instrumentation/attitude-indicator/internal-pitch-deg"] = Double.Parse(tokens[7]);
                data["/instrumentation/attitude-indicator/internal-roll-deg"] = Double.Parse(tokens[8]);
                data["/instrumentation/encoder/indicated-altitude-ft"] = Double.Parse(tokens[9]);
                data["/instrumentation/encoder/pressure-alt-ft"] = Double.Parse(tokens[10]);
                data["/instrumentation/gps/indicated-altitude-ft"] = Double.Parse(tokens[11]);
                data["/instrumentation/gps/indicated-ground-speed-kt"] = Double.Parse(tokens[12]);
                data["/instrumentation/gps/indicated-vertical-speed"] = Double.Parse(tokens[13]);
                data["/instrumentation/heading-indicator/indicated-heading-deg"] = Double.Parse(tokens[14]);
                data["/instrumentation/magnetic-compass/indicated-heading-deg"] = Double.Parse(tokens[15]);
                data["/instrumentation/slip-skid-ball/indicated-slip-skid"] = Double.Parse(tokens[16]);
                data["/instrumentation/turn-indicator/indicated-turn-rate"] = Double.Parse(tokens[17]);
                data["/instrumentation/vertical-speed-indicator/indicated-speed-fpm"] = Double.Parse(tokens[18]);
                data["/controls/flight/aileron"] = Double.Parse(tokens[19]);
                data["/controls/flight/elevator"] = Double.Parse(tokens[20]);
                data["/controls/flight/rudder"] = Double.Parse(tokens[21]);
                data["/controls/flight/flaps"] = Double.Parse(tokens[22]);
                data["/controls/engines/current-engine/throttle"] = Double.Parse(tokens[23]);
                data["/engines/engine/rpm"] = Double.Parse(tokens[24]);
                
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /**
         * Initiliazes the dictionary's values, called upon creation.
         * */
        private void InitializeDataMap()
        {
            data.Add("/position/longitude-deg", 0);
            data.Add("/position/latitude-deg", 0);
            data.Add("/instrumentation/airspeed-indicator/indicated-speed-kt", 0);
            data.Add("/instrumentation/altimeter/indicated-altitude-ft", 0);
            data.Add("/instrumentation/altimeter/pressure-alt-ft", 0);
            data.Add("/instrumentation/attitude-indicator/indicated-pitch-deg", 0);
            data.Add("/instrumentation/attitude-indicator/indicated-roll-deg", 0);
            data.Add("/instrumentation/attitude-indicator/internal-pitch-deg", 0);
            data.Add("/instrumentation/attitude-indicator/internal-roll-deg", 0);
            data.Add("/instrumentation/encoder/indicated-altitude-ft", 0);
            data.Add("/instrumentation/encoder/pressure-alt-ft", 0);
            data.Add("/instrumentation/gps/indicated-altitude-ft", 0);
            data.Add("/instrumentation/gps/indicated-ground-speed-kt", 0);
            data.Add("/instrumentation/gps/indicated-vertical-speed", 0);
            data.Add("/instrumentation/heading-indicator/indicated-heading-deg", 0);
            data.Add("/instrumentation/magnetic-compass/indicated-heading-deg", 0);
            data.Add("/instrumentation/slip-skid-ball/indicated-slip-skid", 0);
            data.Add("/instrumentation/turn-indicator/indicated-turn-rate", 0);
            data.Add("/instrumentation/vertical-speed-indicator/indicated-speed-fpm", 0);
            data.Add("/controls/flight/aileron", 0);
            data.Add("/controls/flight/elevator", 0);
            data.Add("/controls/flight/rudder", 0);
            data.Add("/controls/flight/flaps", 0);
            data.Add("/controls/engines/current-engine/throttle", 0);
            data.Add("/engines/engine/rpm", 0);
        }
    }
}
