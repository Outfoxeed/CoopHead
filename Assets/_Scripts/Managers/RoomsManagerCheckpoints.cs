using OutFoxeed.MonoBehaviourBase;

namespace CoopHead
{
    public partial class RoomsManager : SingletonBase<RoomsManager>
    {
        public Checkpoint[] checkpoints;

        public int GetCheckpointIndex(Checkpoint checkpoint)
        {
            for (int i = 0; i < checkpoints.Length; i++)
            {
                if (checkpoints[i] == checkpoint)
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Return true if checkpoint a is after checkpoint b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public bool IsCheckpointSuperior(Checkpoint a, Checkpoint b) => GetCheckpointIndex(a) > GetCheckpointIndex(b);
    }
}