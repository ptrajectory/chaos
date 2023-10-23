

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


        /// <summary>
        /// Create a channel
        /// </summary>
        /// <param name="organization_id"></param>
        /// <param name="app_id"></param>
        /// <param name="NewChannelData"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<string> createChannel(string organization_id, string app_id, [FromBody] CreateChannel NewChannelData) {
            NewChannelData.OrgID = organization_id;
            NewChannelData.AppID = app_id;

            string id = this.channel.createChannel(NewChannelData);

            return Ok(id);
        }

        /// <summary>
        /// Get a channel
        /// </summary>
        /// <param name="organization_id"></param>
        /// <param name="app_id"></param>
        /// <param name="channelId"></param>
        /// <returns></returns>
        [HttpGet("{channelId}")]
        public ActionResult<GetChannel?> getChannel(string organization_id, string app_id,string channelId){
            GetChannel? the_channel = this.channel.getChannel(channelId); 
            return Ok(the_channel);
        }


        /// <summary>
        /// Update a channel
        /// </summary>
        /// <param name="organization_id"></param>
        /// <param name="app_id"></param>
        /// <param name="channelId"></param>
        /// <param name="NewChannelData"></param>
        /// <returns></returns>
        [HttpPatch("{channelId}")]
        public ActionResult<GetChannel?> updateChannel(string organization_id, string app_id, string channelId, [FromBody] UpdateChannel NewChannelData) {
            GetChannel? channel = this.channel.updateChannel(channelId, NewChannelData);
            return Ok(channel);
        }

        /// <summary>
        /// Add a participant to a channel
        /// </summary>
        /// <param name="organization_id"></param>
        /// <param name="app_id"></param>
        /// <param name="channelId"></param>
        /// <param name="ParticipantData"></param>
        /// <returns></returns>
        [HttpPost("{channelId}/participants")]
        public ActionResult<string> addParticipant(string organization_id, string app_id,string channelId, CreateParticipant ParticipantData){
            string id = this.channel.addParticipant(channelId, ParticipantData);

            return Ok(id);
        }


        /// <summary>
        /// Get all channel participants
        /// </summary>
        /// <param name="organization_id"></param>
        /// <param name="app_id"></param>
        /// <param name="channelId"></param>
        /// <returns></returns>
        [HttpGet("{channelId}/participants")]
        public ActionResult<List<GetUser>> getChannelParticipants(string organization_id, string app_id,string channelId) {
            List<GetUser> participants = this.channel.getChannelParticipants(channelId);
            return Ok(participants);
        }



        /// <summary>
        /// Delete a channel participant
        /// </summary>
        /// <param name="organization_id"></param>
        /// <param name="app_id"></param>
        /// <param name="channelId"></param>
        /// <returns></returns>
        [HttpDelete("{channelId}/participants")]
        public ActionResult<GetParticipant?> deleteChannelParticipant(string organization_id, string app_id,string channelId){
            GetParticipant? participant = this.channel.deleteChannelParticipant(channelId);
            return Ok(participant);
        }


        /// <summary>
        /// Add a message to a channel
        /// </summary>
        /// <param name="organization_id"></param>
        /// <param name="app_id"></param>
        /// <param name="channelId"></param>
        /// <param name="NewMessage"></param>
        /// <returns></returns>
        [HttpPost("{channelId}/messages")]
        public async Task<ActionResult<string>> addMessage(string organization_id, string app_id, string channelId, [FromBody] CreateMessage NewMessage){
            string message_id = await this.channel.addMessage(channelId,NewMessage);
            return Ok(message_id);
        }

        
        /// <summary>
        /// Get a specific message from a channe;
        /// </summary>
        /// <param name="organization_id"></param>
        /// <param name="app_id"></param>
        /// <param name="channelId"></param>
        /// <param name="messageId"></param>
        /// <returns></returns>
        [HttpGet("{channelId}/messages/{messageId}")]
        public ActionResult<GetMessage?> getMessage(string organization_id, string app_id, string channelId, string messageId){
            GetMessage? message = this.channel.getMessage(messageId);

            return Ok(message);
        }


        /// <summary>
        /// Get a channel's messages
        /// </summary>
        /// <param name="organization_id"></param>
        /// <param name="app_id"></param>
        /// <param name="channelId"></param>
        /// <returns></returns>
        [HttpGet("{channelId}/messages")]
        public ActionResult<List<GetMessage>> getMessages(string organization_id, string app_id, string channelId){

            List<GetMessage> messages  = this.channel.getMessages(channelId);

            return Ok(messages);

        }


    }

}