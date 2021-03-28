namespace JpegConverter.Jpeg.FileStructure.Segments
{
    using System;
    using System.Linq;
    using Jpeg.Structures;
    using Markers;
    using Structures;

    internal class ScanHeader
    {
        // SOS marker: FFDA 
        public JpegMarker SegmentMarker = new JpegMarker(0xDA);
        
        public ushort Ls;

        public byte Ns;

        public ScanComponentSpecificationParameters[] ScanCspArray;

        public byte Ss;

        public byte Sc;

        public ByteHalf Ah;

        public ByteHalf Al;

        public byte[] ToBytes()
        {
            return SegmentMarker.ToBytes()
                .Concat(BitConverter.GetBytes(Ls))
                .Concat(new []{Ns})
                .Concat(ScanCspArray.SelectMany(csp => csp.ToBytes()))
                .Concat(new []
                {
                    Ss,
                    Sc,
                    ByteHalf.Join(Ah,Al)
                })
                .ToArray();
        }
    }
}