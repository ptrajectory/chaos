

using chaos.Dtos.Channel;
using chaos.Dtos.Message;
using chaos.Dtos.Participant;
using chaos.Dtos.User;
using chaos.Services;
using Microsoft.AspNetCore.Mvc;
using chaos.AuthRepository;
using Microsoft.AspNetCore.Authorization;

namespace chaos.Controllers {

    [ApiController]
    [Route("api/organizations/{organization_id}/apps/{app_id}/channels")]
    [Authorize]
    [RequiresClaim(claimName: IdentityData.OwnerClaimName, param: "app_id", claimValue: null)]
    public class Channel: ControllerBase
    {

        private readonly IChannel channel;


        public Channel(IChannel _channel) {
            this.channel = _channel;
        }


        [HttpPost]
        public ActionResult<string> createChannel(string organization_id, string app_id, [FromBody] CreateChannel NewChannelData) {
            NewChannelData.OrgID = organization_id;
            NewChannelData.AppID = app_id;

            string id = this.channel.createChannel(NewChannelData);

            return Ok(id);
        }


        [HttpGet("{channelId}")]
        public ActionResult<GetChannel?> getChannel(string organization_id, string app_id,string channelId){
            GetChannel? the_channel = this.channel.getChannel(channelId); 
            return Ok(the_channel);
        }

        [HttpPatch("{channelId}")]
        public ActionResult<GetChannel?> updateChannel(string organization_id, string app_id, string channelId, [FromBody] UpdateChannel NewChannelData) {
            GetChannel? channel = this.channel.updateChannel(channelId, NewChannelData);
            return Ok(channel);
        }

        [HttpPost("{channelId}/participants")]
        public ActionResult<string> addParticipant(string organization_id, string app_id,string channelId, CreateParticipant ParticipantData){
            string id = this.channel.addParticipant(channelId, ParticipantData);

            return Ok(id);
        }


        [HttpGet("{channelId}/participants")]
        public ActionResult<List<GetUser>> getChannelParticipants(string organization_id, string app_id,string channelId) {
            List<GetUser> participants = this.channel.getChannelParticipants(channelId);
            return Ok(participants);
        }


        [HttpDelete("{channelId}/participants")]
        public ActionResult<GetParticipant?> deleteChannelParticipant(string organization_id, string app_id,string channelId){
            GetParticipant? participant = this.channel.deleteChannelParticipant(channelId);
            return Ok(participant);
        }


        [HttpPost("{channelId}/messages")]
        public ActionResult<string> addMessage(string organization_id, string app_id, string channelId, [FromBody] CreateMessage NewMessage){
            string message_id = this.channel.addMessage(channelId,NewMessage);
            return Ok(message_id);
        }


        [HttpGet("{channelId}/messages/{messageId}")]
        public ActionResult<GetMessage?> getMessage(string organization_id, string app_id, string channelId, string messageId){
            GetMessage? message = this.channel.getMessage(messageId);

            return Ok(message);
        }


        [HttpGet("{channelId}/messages")]
        public ActionResult<List<GetMessage>> getMessages(string organization_id, string app_id, string channelId){

            List<GetMessage> messages  = this.channel.getMessages(channelId);

            return Ok(messages);

        }


    }

}