﻿
using System.Collections.Generic;
using MusicPlayer.Model;

namespace MusicPlayer.Services.PlayService
{
    public interface IPlayAudio
    {
        void StartPlayTrack();
        void StartPlayTrack(TrackModel model);
        void ContinuePlayTrack();
        void PauseTrack();
        void StopTrack();
        int CurrentTrackProgressPosition();

        void NextPlayTrack();
        void PrevPlayTrack();

        TrackModel GetCurrentTrackModel();
        List<TrackModel> GetTrackModelList();

        void SetVolume(bool level);
    }
}